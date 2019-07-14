using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public abstract class ImageGrid : Control
    {
        private Timer _refreshTimer;
        private bool _backgroundRepaintRequired;
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
        private object _backgroundBrushObj = new object();
        private SolidBrush _backgroundBrush;

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
            //
            _column = 3;
            _gutter = 0;
            _overlayBrush = new SolidBrush(Color.FromArgb(40, Color.Black));

            _imageWrappers = new List<ImageWrapper>();

            //
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 40; // 1000/25
            _refreshTimer.Tick += _refreshTimer_Tick;

            _refreshTimer.Start();
        }

        private bool _isBusy;
        void _refreshTimer_Tick(object sender, EventArgs e)
        {
            
            // multi thread
            // adjust the drawing region
            foreach (var img in _imageWrappers)
            {
                int diffY = img.ClippingRegion.Y - img.DrawingRegion.Y;
                int adjustY = Math.Min(4, Math.Abs(diffY)) * Math.Sign(diffY);
                img.DrawingRegion = img.DrawingRegion.AdjustY(adjustY);
            }
            //

            if (!DesignMode)
            {
                // double buffered
                if (_isBusy) return;
                _isBusy = true;
                using(var buffer = new Bitmap(this.Width, this.Height))
                {
                    
                    // foreground
                    using (var g = Graphics.FromImage(buffer))
                    {
                        // background
                        if (_backgroundRepaintRequired)
                        {
                            _backgroundRepaintRequired = false;
                            g.FillRectangle(_backgroundBrush, this.ClientRectangle);
                        }

                        PaintImages(g, GetImagesInView());
                    }
                    Draw(graphic => graphic.DrawImage(buffer, this.ClientRectangle));
                }
                _isBusy = false;
            }
        }

        private void Draw(Action<Graphics> drawAction)
        {
            using (var g = this.CreateGraphics())
            {
                drawAction(g);
            }
        }

        // public methods
        public void Clear()
        {
            DisposeImages();
            _imageWrappers = new List<ImageWrapper>();
            _offsetY = 0;

            _backgroundRepaintRequired = true;
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
            _imageWrappers.Add(new ImageWrapper(image) { ResizedImage = BitmapHelper.ResizeImage(image, _columnWidth, availableHeight) });
            // each time an image added, we calculate entire image position => waste, slowdown performance
            ReDraw();
        }

        // calculate and drawing images
        public void ReDraw()
        {
            _virtualHeight = 0;
            if (_imageWrappers != null && _imageWrappers.Count != 0)
                ComputePosition();
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

            // if scrolled then reset the background
            if (changed != 0)
                _backgroundRepaintRequired = true;
            // because clipping region changed, then image will be re-drawn
            // so reset the background doesn't replace the image region
            foreach (ImageWrapper iw in _imageWrappers)
                iw.ClippingRegion = iw.ClippingRegion.AdjustY(-changed);
        }
        //
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateColumnWidth();
            ReDraw();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            lock (_backgroundBrushObj)
            {
                if (_backgroundBrush != null)
                    _backgroundBrush.Dispose();
                _backgroundBrush = new SolidBrush(this.BackColor);
            }
        }

        private void DisposeImages()
        {
            if (_imageWrappers != null && _imageWrappers.Count > 0)
            {
                foreach (var imageWrapper in _imageWrappers)
                    imageWrapper.Dispose();
            }
        }
        //
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                DisposeImages();
                _backgroundBrush.Dispose();
                _overlayBrush.Dispose();
                _refreshTimer.Stop();
                _refreshTimer.Dispose();
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
                if (_imageWrappers[i].ClippingRegion.Contains(location))
                    return i;
            }
            return -1;
        }
        private IEnumerable<ImageWrapper> GetImagesInView()
        {
            foreach (var iw in _imageWrappers)
            {
                if (iw.RedrawRequired && iw.ClippingRegion.IntersectsWith(this.ClientRectangle))
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
                Image = itemRef.ResizedImage;
                Boundary = itemRef.ClippingRegion;

                //
                PickedLocation = pickedPosition;
                _originBoundary = itemRef.ClippingRegion;
            }

            public void Move(Point p)
            {
                Boundary.X = _originBoundary.X + (p.X - PickedLocation.X);
                Boundary.Y = _originBoundary.Y + (p.Y - PickedLocation.Y);
            }
        }
    }
}