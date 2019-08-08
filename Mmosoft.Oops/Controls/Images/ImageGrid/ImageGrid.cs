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
        private Timer _repaintTimer;
        private bool _bgRepaint;
        private bool _painting;
        // -- private members
        protected List<Img> _imgs;
        // layout
        protected int _column;
        protected int _gutter;
        protected int _colWidth;
        protected int _virtualHeight;
        protected int _offsetY;
        protected DraggingItem _dragItem;
        
        protected int _selectedIndex;
        private object _bgBrushObj = new object();
        private SolidBrush _bgBrush;

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
                if (0 <= value && value < _imgs.Count)
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
                return _imgs.Count; 
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
            _imgs = new List<Img>();
            _bgBrush = new SolidBrush(this.BackColor);
            //
            _repaintTimer = new Timer();
            _repaintTimer.Interval = 24;
            _repaintTimer.Tick += _refreshTimer_Tick;

            _repaintTimer.Start();
        }


        void _refreshTimer_Tick(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // multi thread
            // adjust the drawing region
            foreach (var img in _imgs)
            {
                int diffY = img.DrawingRegion.Y - img.ClippingRegion.Y;
                int adjustY = Math.Min(4, Math.Abs(diffY));
                int delta = diffY  - adjustY;
                img.DrawingRegion = img.DrawingRegion.AdjustSize(-adjustY, -adjustY);
                img.DrawingRegion = img.ClippingRegion.AdjustSizeFromCenter(delta, delta);
            }
            if (_painting) return;
            _painting = true;
            using (var buffer = new Bitmap(this.Width, this.Height))
            {
                using (var g = Graphics.FromImage(buffer))
                {
                    if (_bgRepaint)
                    {
                        _bgRepaint = false;
                        g.FillRectangle(_bgBrush, this.ClientRectangle);
                    }
                    PaintImages(g, GetImagesInView());
                }
                Draw(graphic => graphic.DrawImage(buffer, this.ClientRectangle));
            }
            _painting = false;
        }

        private void Draw(Action<Graphics> drawAction)
        {
            using (var g = this.CreateGraphics())
                drawAction(g);
        }

        // public methods
        public void Clear()
        {
            DisposeImages();
            _imgs = new List<Img>();
            _offsetY = 0;
            _bgRepaint = true;
        }
        public void Add(Image image)
        {
            if (_imgs == null) _imgs = new List<Img>();
            int availableHeight = (int)((_colWidth * 1f / image.Width) * image.Height);
            Image resized = BitmapHelper.ResizeImage(image, _colWidth, availableHeight);
            _imgs.Add(new Img(image, resized));
            ReDraw();
        }

        // calculate and drawing images
        public void ReDraw()
        {
            _virtualHeight = 0;
            if (_imgs != null && _imgs.Count != 0)
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
                    _dragItem = new DraggingItem(_imgs[pickedImageIndex], e.Location);
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
                    droppedIndex = _imgs.Count - 1;

                // and only swap if the user doesn't click to another non-client rectangle
                if (droppedIndex != -1)
                {
                    // swap if picked != dropped
                    if (droppedIndex != _dragItem.Index)
                    {
                        _imgs[_dragItem.Index] = _imgs[droppedIndex];
                        _imgs[droppedIndex] = _dragItem.ItemRef;
                    }
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
                            Image = _imgs[this.SelectedIndex].Original
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
            this.Cursor = GetImageIndex(e.Location) < 0 ? Cursors.Default : Cursors.Hand;
            if (_dragItem != null)
                _dragItem.Move(e.Location);
        }                
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_virtualHeight < this.Height) return;
            int newOffsetY = _offsetY - e.Delta;
            if (newOffsetY < 0) newOffsetY = 0;
            if (newOffsetY > _virtualHeight - this.Height)
                newOffsetY = _virtualHeight - this.Height;
            int changed = newOffsetY - _offsetY;
            _offsetY = newOffsetY;
            // if scrolled then reset the background
            if (changed != 0)
                _bgRepaint = true;
            // because clipping region changed, then image will be re-drawn
            // so reset the background doesn't replace the image region
            foreach (Img iw in _imgs)
                iw.ClippingRegion = iw.ClippingRegion.AdjustY(-changed);
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateColumnWidth();
            ReDraw();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            lock (_bgBrushObj)
            {
                if (_bgBrush != null)
                    _bgBrush.Dispose();
                _bgBrush = new SolidBrush(this.BackColor);
            }
        }

        private void DisposeImages()
        {
            if (_imgs != null && _imgs.Count > 0)
            {
                foreach (var imageWrapper in _imgs)
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
                _bgBrush.Dispose();
                _repaintTimer.Stop();
                _repaintTimer.Dispose();
                Clear();
            }
        }
        //
        protected abstract void ComputePosition();
        protected abstract void PaintImages(Graphics g, IEnumerable<Img> imageWrappers);

        // --- private methods
        private void UpdateColumnWidth()
        {
            _colWidth = (int)((this.Width - 1 - (_column + 1) * _gutter * 1f) / _column);
            if (_column < 0)
                throw new Exception("Column width is <= 0, please check your setting again.");
        }
        private int GetImageIndex(Point location)
        {
            if (_imgs == null) return -1;
            for (int i = 0; i < _imgs.Count; i++)
            {
                if (_imgs[i].ClippingRegion.Contains(location))
                    return i;
            }
            return -1;
        }
        private IEnumerable<Img> GetImagesInView()
        {
            foreach (var iw in _imgs)
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
            public Img ItemRef;
            public Rectangle Boundary;
            public Image Image;
            public int Index;
            
            public DraggingItem(Img itemRef, Point pickedPosition)
            {
                ItemRef = itemRef;
                Image = itemRef.Resized;
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