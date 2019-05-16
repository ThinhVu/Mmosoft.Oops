using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Mmosoft.Oops.Animation;

namespace Mmosoft.Oops.Controls
{
    public class ImageGrid : Control
    {
        private List<ImageWrapper> _imgWrappers;
        private Animation.Animator _scrollAnimator;
        private int _virtualHeight;
        private int _offsetY;
        private bool _painting;

        private int _imgPadding;
        [Browsable(true)]
        [Description("Pixel between each image")]
        public int ImagePadding
        {
            get { return _imgPadding; }
            set { _imgPadding = value; }
        }

        private int _column;
        [Browsable(true)]
        [Description("Number of column will be displayed in image grid")]
        public int Column
        {
            get { return _column; }
            set { _column = value; ReDraw(); }
        }        
       
        private int _selectedIndex;
        [Browsable(true)]
        [Description("Get or set index of selected image")]
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (0 <= value && value < _imgWrappers.Count)
                {
                    _selectedIndex = value;
                    // perform click
                    if (OnItemClicked != null)
                    {                        
                        OnItemClicked(this, new ImageGridItemClickedEventArgs()
                        {
                            Index = value,
                            Image = _imgWrappers[value].Image
                        });
                    }

                    if (_autoScrollToSelectedImage)
                        ScrollToSelectedImage();
                }
            }
        }

        [Browsable(true)]
        [Description("Get total image number")]
        public int Count 
        { 
            get 
            { 
                return _imgWrappers.Count; 
            } 
        }

        private bool _autoScrollToSelectedImage;
        [Browsable(true)]
        [Description("Enabled/disabled auto scroll to selected image.")]
        public bool AutoScrollToSelectedImage
        {
            get
            {
                return _autoScrollToSelectedImage;
            }
            set
            {
                _autoScrollToSelectedImage = value;
            }
        }

        // events
        public event ImageGridItemClickedEventHandler OnItemClicked;


        // methods
        public ImageGrid()
        {            
            _column = 3;
            _imgPadding = 5;
            _imgWrappers = new List<ImageWrapper>();
            //
            DoubleBuffered = true;            
        }

        public void Load(List<Image> imgs)
        {
            _virtualHeight = 0;
            _offsetY = 0;
            _imgWrappers = imgs.Select(img => new ImageWrapper(img)).ToList();            
            ReDraw();
        }        
        public void ReDraw()
        {
            if (_imgWrappers == null || _imgWrappers.Count == 0)
                return;

            int imgWidth = (int)((this.Width - 1 - (Column + 1) * _imgPadding * 1f) / _column);
            var columnsHeight = new int[Column];
            for (int i = 0; i < columnsHeight.Length; i++)
            {
                columnsHeight[i] = _imgPadding;
            }
            
            int x, y, colId;
            for (int i = 0; i < _imgWrappers.Count; i++)
            {               
                GetMinHeightAndColumnIndex(columnsHeight, out y, out colId);
                x = _imgPadding * (1 + colId) + imgWidth * colId;

                ImageWrapper iw = _imgWrappers[i];
                int actualImgWidth = iw.Image.Width;
                int actualImgHeight = iw.Image.Height;
                float hwRatio = 1f * actualImgHeight / actualImgWidth;
                int height = (int)(imgWidth * hwRatio);
                iw.Boundary = new Rectangle(x, y, imgWidth, height);
                // next image in the same column
                columnsHeight[colId] += height + _imgPadding;

                if (_virtualHeight < columnsHeight[colId])
                    _virtualHeight = columnsHeight[colId];
            }

            Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            _virtualHeight = 0;
            ReDraw();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var hitPoint = e.Location.ChangePosition(0, _offsetY);  
            this.Cursor = GetHotItemIndex(hitPoint) < 0 ? Cursors.Default : Cursors.Hand;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var hitPoint = e.Location.ChangePosition(0, _offsetY);
                this.SelectedIndex = GetHotItemIndex(hitPoint);                
            }            
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            _offsetY -= e.Delta;
            //
            if (_offsetY < 0)
                _offsetY = 0;

            // 
            if (_offsetY > _virtualHeight - this.Height)
                _offsetY = _virtualHeight - this.Height;
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_painting)
                return;
            _painting = true;
            var g = e.Graphics;
            if (DesignMode)
            {
                g.DrawRectangle(Pens.Black, this.ClientRectangle.ChangeSizeRelative(-1, -1));
                g.DrawString(this.Name + " control doesn't provide design time support", this.Font, Brushes.Black, new Point(0, 0));
            }
            else
            {
                if (_imgWrappers != null)
                {
                    foreach (ImageWrapper i in GetImageInViewport())
                    {
                        g.DrawImage(i.Image, i.Boundary);
                    }
                }
            }
            _painting = false;
        }

        public void ScrollToSelectedImage()
        {
            this.ScrollToSelectedImage(_imgWrappers[SelectedIndex].Boundary);
        }

        private void ScrollToSelectedImage(Rectangle boundary)
        {
            int movePixel = 5;

            if (_scrollAnimator != null)
                _scrollAnimator.Stop();

            // img height less than viewport height
            if (boundary.Height < this.Height)
            {
                // image above the viewport or intesect with below part of an image
                // then we need to decrease offset to show the image
                if (   _offsetY > boundary.Bottom
                    || (_offsetY < boundary.Bottom && _offsetY > boundary.Top))
                {
                    _scrollAnimator = new Animation.Animator();
                    _scrollAnimator.Add(new Step
                    {
                        TotalStep = (_offsetY - boundary.Top) / movePixel,
                        Interval = 24,
                        AnimAction = (i) =>
                        {
                            _offsetY -= movePixel;
                            Invalidate();
                        }
                    });

                    _scrollAnimator.Start();
                }
                // entire or part of an image below the viewport                
                else if (_offsetY + this.Height < boundary.Bottom)
                {
                    _scrollAnimator = new Animation.Animator();
                    _scrollAnimator.Add(new Step
                    {
                        TotalStep = (boundary.Bottom - _offsetY - this.Height) / movePixel,
                        Interval = 24,
                        AnimAction = (i) =>
                        {
                            _offsetY += movePixel;
                            Invalidate();
                        }
                    });

                    _scrollAnimator.Start();
                }
            }
        }
        private void GetMinHeightAndColumnIndex(int[] num, out int value, out int index)
        {
            value = int.MaxValue;
            index = -1;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (value >= num[i])
                {
                    value = num[i];
                    index = i;
                }
            }
        }
        private int GetHotItemIndex(Point location)
        {
            for (int i = 0; i < _imgWrappers.Count; i++)
            {
                if (_imgWrappers[i].Boundary.Contains(location))
                    return i;
            }

            return -1;            
        }
        private IEnumerable<ImageWrapper> GetImageInViewport()
        {
            Rectangle r;
            foreach (var iw in _imgWrappers)
            {
                r = iw.Boundary.MoveY( - _offsetY);
                if (r.IntersectsWith(this.ClientRectangle))
                    yield return new ImageWrapper(iw.Image) { Boundary = r };
            }
        }
    }

    public delegate void ImageGridItemClickedEventHandler(object sender, ImageGridItemClickedEventArgs e);

    public class ImageGridItemClickedEventArgs : EventArgs
    {
        public Image Image { get; set; }
        public int Index { get; set; }
    }
}