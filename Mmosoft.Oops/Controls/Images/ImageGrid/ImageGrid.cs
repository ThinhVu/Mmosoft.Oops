using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public abstract class ImageGrid : Control
    {
        // -- private members
        protected List<ImageWrapper> _imageWrappers;
        // layout
        protected int _column;
        protected int _gutter;
        protected int _columnWidth;
        protected int _virtualHeight;
        protected int _offsetY;
        // drag drop
        protected DraggingItem _dragItem;
        // focus on hovering image
        protected ImageWrapper _hoverItem;
        protected SolidBrush _overlayBrush;
        protected int _hoverIndex;
        // 
        protected int _selectedIndex;

        // -- properties
        [Browsable(true)]
        [Description("Get or set index of selected image")]
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            private set
            {
                if (0 <= value && value < _imageWrappers.Count)
                {
                    _selectedIndex = value;
                }
            }
        }

        [Browsable(true)]
        [Description("Get total image number")]
        public int ImageCount 
        { 
            get 
            { 
                return _imageWrappers.Count; 
            }
        }

        [Browsable(true)]
        public int Column
        {
            get
            {
                return _column;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Column must greater than 0.");
                if (_column != value)
                {
                    _column = value;
                    UpdateColumnWidth();
                    ReDraw();
                }
            }
        }

        [Browsable(true)]
        public int Gutter 
        {
            get
            {
                return _gutter;
            }
            set
            {
                if (_gutter < 0)
                    throw new ArgumentException("Gutter must not nagative");
                if (_gutter != value)
                {
                    _gutter = value;
                    UpdateColumnWidth();
                    ReDraw();
                }
            }
        }

        [Browsable(true)]
        public bool AllowDragDrop
        {
            get;
            set;
        }

        // events
        public event ImageGridItemClickedEventHandler OnImageClicked;

        // methods
        public ImageGrid()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            //
            _column = 3;
            _gutter = 0;
            _overlayBrush = new SolidBrush(Color.FromArgb(40, Color.Black)); 
        }

        // public methods
        public void Clear()
        {
            if (_imageWrappers != null && _imageWrappers.Count > 0)
            {
                foreach (var imageWrapper in _imageWrappers)
                    imageWrapper.Dispose();
            }
            _imageWrappers = new List<ImageWrapper>();
            _offsetY = 0;

            // clear paint
            ReDraw();
        }
        public void Add(Image image)
        {
            if (_imageWrappers == null)
                _imageWrappers = new List<ImageWrapper>();
            // make a copy of original image
            int actualImageWidth = image.Width;
            int actualImageHeight = image.Height;
            //
            int availableHeight = (int)((_columnWidth * 1f / actualImageWidth) * actualImageHeight);
            //
            _imageWrappers.Add(new ImageWrapper(image){ ResizedImage = BitmapHelper.ResizeImage(image, actualImageWidth, availableHeight) });
            // each time an image added, we calculate entire image position => waste, slowdown performance
            ReDraw();
        }

        // calculate and drawing images
        public void ReDraw()
        {
            _virtualHeight = 0;
            if (_imageWrappers != null && _imageWrappers.Count != 0)
                ComputePosition();
            Invalidate();
        }

        // event handlers
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            // Allow the user scroll image grid view even though 
            // a typing cursor is currently focusing on another controls
            this.Focus();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // listen for mouse click or starting to drag
            base.OnMouseDown(e);
            if (AllowDragDrop)
            {
                int pickedImageIndex = GetImageIndex(e.Location);
                // check with -1 to ignore when the user clicking to blank zone
                if (pickedImageIndex > -1)
                {
                    _dragItem = new DraggingItem(_imageWrappers[pickedImageIndex], e.Location);
                    _dragItem.Index = pickedImageIndex;
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            // checkin if swap is needed
            if (_dragItem != null && e.Location != _dragItem.PickedLocation)
            {
                int droppedIndex = GetImageIndex(e.Location); 
                // check if the user drag an image then drop into blank zone in grid view
                // then move the dragged item into the end of image list
                if (droppedIndex == -1 && this.Bounds.Contains(e.Location))
                    droppedIndex = _imageWrappers.Count - 1;

                // and only swap if the user doesn't click to another non-client rectangle
                if (droppedIndex != -1)
                {
                    // swap if picked != dropped
                    if (droppedIndex != _dragItem.Index)
                    {
                        _imageWrappers[_dragItem.Index] = _imageWrappers[droppedIndex];
                        _imageWrappers[droppedIndex] = _dragItem.ItemRef;
                    }

                    // update hovering items
                    _hoverIndex = droppedIndex;
                    _hoverItem = _imageWrappers[droppedIndex];
                }
                
                ReDraw();
            }
            else // click
            {
                this.SelectedIndex = GetImageIndex(e.Location);
                // in case click, change selected index to invoke index changed event
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    // perform click
                    if (OnImageClicked != null)
                    {
                        OnImageClicked(this, new ImageGridItemClickedEventArgs()
                        {
                            Index = this.SelectedIndex,
                            Image = _imageWrappers[this.SelectedIndex].OriginalImage
                        });
                    }
                }
            }

            // then un-link drag item
            _dragItem = null;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int hoveringIndex = GetImageIndex(e.Location);
            bool hoverChanged = (hoveringIndex > -1 && _hoverIndex != hoveringIndex);
            this.Cursor = hoveringIndex < 0 ? Cursors.Default : Cursors.Hand;

            bool shouldReDrawn = _dragItem != null || hoverChanged;
            
            if (_dragItem != null) 
                _dragItem.Move(e.Location);

            if (hoverChanged) 
            { 
                _hoverIndex = hoveringIndex; 
                _hoverItem = _imageWrappers[hoveringIndex]; 
            }
            
            if (shouldReDrawn) ReDraw();
        }                
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_virtualHeight < this.Height)
                return;

            var newOffsetY = _offsetY - e.Delta;
            //
            if (newOffsetY < 0)
                newOffsetY = 0;

            // 
            if (newOffsetY > _virtualHeight - this.Height)
                newOffsetY = _virtualHeight - this.Height;

            int changed = newOffsetY - _offsetY;
            _offsetY = newOffsetY;

            foreach (ImageWrapper iw in _imageWrappers)
            {
                iw.Boundary = iw.Boundary.MoveY(-changed);
            }
            Invalidate();
        }
        //
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateColumnWidth();
            ReDraw();
        }
        //
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            if (DesignMode)
            {
                g.DrawString(
                    this.Name + " control doesn't provide design time support", 
                    this.Font, 
                    Brushes.Black, new Point(0, 0));

                g.DrawRectangle(Pens.Black, this.ClientRectangle.DecreaseSize(1, 1));
            }
            else
            {
                PaintImages(g, GetImagesInView());
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _overlayBrush.Dispose();
                Clear();
            }
        }
        //
        protected abstract void ComputePosition();
        protected abstract void PaintImages(Graphics g, IEnumerable<ImageWrapper> imageWrappers);

        // --- private methods
        private void UpdateColumnWidth()
        {
            _columnWidth = (int)((this.Width - 1 - (_column + 1) * _gutter * 1f) / _column);
            if (_column < 0)
                throw new Exception("Column width is <= 0, please check your setting again.");
        }
        private int GetImageIndex(Point location)
        {
            if (_imageWrappers == null) return -1;
            for (int i = 0; i < _imageWrappers.Count; i++)
            {
                if (_imageWrappers[i].Boundary.Contains(location))
                    return i;
            }
            return -1;
        }
        private IEnumerable<ImageWrapper> GetImagesInView()
        {
            foreach (var iw in _imageWrappers)
            {
                if (iw.Boundary.IntersectsWith(this.ClientRectangle))
                    yield return iw;
            }
        }

        protected class DraggingItem
        {
            private Rectangle _originBoundary;
            //
            public Point PickedLocation;
            public ImageWrapper ItemRef;
            public Rectangle Boundary;
            public Image Image;
            public int Index;
            
            public DraggingItem(ImageWrapper itemRef, Point pickedPosition)
            {
                ItemRef = itemRef;
                Image = itemRef.OriginalImage;
                Boundary = itemRef.Boundary;

                //
                PickedLocation = pickedPosition;
                _originBoundary = itemRef.Boundary;
            }

            public void Move(Point p)
            {
                Boundary.X = _originBoundary.X + (p.X - PickedLocation.X);
                Boundary.Y = _originBoundary.Y + (p.Y - PickedLocation.Y);
            }
        }
    }
}