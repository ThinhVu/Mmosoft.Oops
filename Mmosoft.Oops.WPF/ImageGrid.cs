using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mmosoft.Oops.WPF
{
    public class ImageGrid : FrameworkElement
    {
        private System.Windows.Threading.DispatcherTimer _repaintTimer;
        protected int _redrawRequestId;
        // -- private members
        protected List<Img> _imgs;
        // layout
        protected int _column;
        protected int _gutter;
        protected int _colWidth;
        protected double _virtualHeight;
        protected double _offsetY;
        
        protected int _selectedIndex;

        // -- properties
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

        public int ImageCount 
        { 
            get 
            { 
                return _imgs.Count; 
            }
        }

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
                    UpdateColumnWidth(this.Width);
                    ReDraw();
                }
            }
        }

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
                    UpdateColumnWidth(this.Width);
                    ReDraw();
                }
            }
        }

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
            _column = 3;
            _gutter = 3;
            _imgs = new List<Img>();
            _repaintTimer = new System.Windows.Threading.DispatcherTimer();
            _repaintTimer.Interval = TimeSpan.FromMilliseconds(40);
            _repaintTimer.Tick += _refreshTimer_Tick;
            _repaintTimer.Start();
        }

        void _refreshTimer_Tick(object sender, EventArgs e)
        {
            // multi thread
            // adjust the drawing region
            foreach (var img in _imgs)
            {
                double diffY = img.DrawingRegion.Y - img.ClippingRegion.Y;
                double adjustY = Math.Min(4, Math.Abs(diffY));
                double delta = diffY - adjustY;
                img.DrawingRegion = img.ClippingRegion.AdjustSizeFromCenter(delta, delta);
            }
            InvalidateVisual();
        }

        // public methods
        public void Clear()
        {
            _imgs = new List<Img>();
            _offsetY = 0;
        }

        // multi-threaded
        public void Add(BitmapImage image)
        {
            if (_imgs == null) _imgs = new List<Img>();
            _imgs.Add(new Img(image, image));
            ReDraw();
        }

        // calculate and drawing images
        public void ReDraw()
        {
            _redrawRequestId++;
            if (_redrawRequestId == int.MaxValue)
                _redrawRequestId = 0;

            _virtualHeight = 0;
            if (_imgs != null && _imgs.Count != 0)
                ComputePosition(_redrawRequestId);
        }

        // event handlers
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            // Allow the user scroll image grid view even though 
            // a typing cursor is currently focusing on another controls
            this.Focus();
        }
        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.SelectedIndex = GetImageIndex(e.GetPosition(this));
            if (OnImageClicked != null)
            {
                OnImageClicked(this, new ImageGridItemClickedEventArgs()
                {
                    Index = this.SelectedIndex,
                    Image = _imgs[this.SelectedIndex].Original
                });
            }
        }
        protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            base.OnMouseWheel(e);
            if (_virtualHeight < this.Height) return;
            double newOffsetY = _offsetY - e.Delta;
            if (newOffsetY < 0) newOffsetY = 0;
            if (newOffsetY > _virtualHeight - this.Height)
                newOffsetY = _virtualHeight - this.Height;
            double changed = newOffsetY - _offsetY;
            _offsetY = newOffsetY;
            // if scrolled then reset the background
            // because clipping region changed, then image will be re-drawn
            // so reset the background doesn't replace the image region
            foreach (Img iw in _imgs)
                iw.ClippingRegion = iw.ClippingRegion.AdjustY(-changed);
        }
        //
        protected void ComputePosition(int currentReDrawRequestId)
        {
            var stackTops = new double[Column];
            for (int i = 0; i < stackTops.Length; i++)
                stackTops[i] = Gutter;

            double left, top;
            List<int> colId;
            for (int i = 0; i < _imgs.Count; i++)
            {
                // new redraw request has been called, skip current redraw
                if (currentReDrawRequestId < _redrawRequestId)
                    return;

                GetMinStackHeightAndIndex(stackTops, out top, out colId);
                left = Gutter * (1 + colId[0]) + _colWidth * colId[0];

                Img iw = _imgs[i];
                double actualImageWidth = iw.Original.Width;
                double actualImageHeight = iw.Original.Height;
                double availableHeight = (int)((_colWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.ClippingRegion = new Rect(left, top - _offsetY, _colWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                stackTops[colId[0]] += availableHeight + Gutter;

                // increase virtual height
                if (_virtualHeight < stackTops[colId[0]])
                    _virtualHeight = stackTops[colId[0]];
            }
        }
        private void GetMinStackHeightAndIndex(double[] num, out double value, out List<int> indexes)
        {
            value = int.MaxValue;
            indexes = new List<int>();
            // looping from right to left
            // so left side will have higher priority
            for (int i = 0; i < num.Length; i++)
            {
                if (value > num[i])
                {
                    indexes.Clear();
                    value = num[i];
                    indexes.Add(i);
                }
                else if (value == num[i])
                {
                    indexes.Add(i);
                }
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateColumnWidth(sizeInfo.NewSize.Width);
        }

        private void UpdateColumnWidth(double newWidth)
        {
            _colWidth = (int)((newWidth - (_column + 1) * _gutter * 1f) / _column);
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
                // TODO
                if (iw.RedrawRequired /*&& iw.ClippingRegion.IntersectsWith(thi)*/)
                    yield return iw;
            }
        }

        public class ImageGridItemClickedEventArgs : EventArgs
        {
            public BitmapImage Image { get; set; }
            public int Index { get; set; }
        }

        public delegate void ImageGridItemClickedEventHandler(object sender, ImageGridItemClickedEventArgs e);

        public class Img
        {
            public bool RedrawRequired;
            private BitmapImage _original;
            private BitmapImage _resized;
            private Rect _clippingRegion;
            private Rect _drawingRegion;

            /// <summary>
            /// Original image src
            /// </summary>
            public BitmapImage Original
            {
                get
                {
                    return _original;
                }
                set
                {
                    if (_original != value)
                    {
                        _original = value;
                        RedrawRequired = true;
                    }
                }
            }
            /// <summary>
            /// Image which has been resized to increase performance of painting
            /// </summary>
            public BitmapImage Resized
            {
                get
                {
                    return _resized;
                }
                set
                {
                    if (_resized != value)
                    {
                        _resized = value;
                        RedrawRequired = true;
                    }
                }
            }
            /// <summary>
            /// An area which will be used to draw
            /// </summary>
            public Rect ClippingRegion
            {
                get
                {
                    return _clippingRegion;
                }
                set
                {
                    if (_clippingRegion != value)
                    {
                        _clippingRegion = value;

                        if (_drawingRegion == Rect.Empty)
                            _drawingRegion = _clippingRegion.AdjustSizeFromCenter(16, 16);
                        else
                            _drawingRegion = _clippingRegion;
                    }
                }
            }
            /// <summary>
            /// Area which image will be draw in
            /// When animating, DrawingRegion area are contained in Clipping region
            /// When animating completed, DrawRegion is equal to ClippingRegion
            /// </summary>
            public Rect DrawingRegion
            {
                get
                {
                    return _drawingRegion;
                }
                set
                {
                    if (_drawingRegion != value)
                    {
                        _drawingRegion = value;
                        RedrawRequired = true;
                    }
                }
            }
            public Img(BitmapImage original)
            {
                Original = original;
            }
            public Img(BitmapImage original, BitmapImage resized)
            {
                Original = original;
                Resized = resized;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            foreach (Img image in GetImagesInView())
            {
                //Geometry clippingRegion = new RectangleGeometry(image.ClippingRegion);
                //drawingContext.PushClip(clippingRegion);
                drawingContext.DrawImage(image.Original, image.DrawingRegion);
            }
        }
    }

    public static class RectEx
    {
        public static Rect AdjustSizeFromCenter(this Rect r, double deltaW, double deltaH)
        {
            return new Rect(r.X - deltaW / 2, r.Y - deltaH / 2, r.Width + deltaW, r.Height + deltaH);
        }

        public static Rect AdjustY(this Rect r, double y)
        {
            return AdjustXY(r, 0, y);
        }
        public static Rect AdjustXY(this Rect r, double x, double y)
        {
            return new Rect(r.X + x, r.Y + y, r.Width, r.Height);
        }
    }
}
