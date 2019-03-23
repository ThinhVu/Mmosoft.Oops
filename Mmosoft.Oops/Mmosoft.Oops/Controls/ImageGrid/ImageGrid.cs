using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public class ImageGrid : Control
    {        
        private List<ImageWrapper> _imgWrappers;
        private int _virtualHeight;
        private int _offsetY;

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

        public event ImageGridItemClickedEventHandler OnItemClicked;

        public ImageGrid()
        {            
            _column = 3;
            _imgPadding = 5;
            //
            DoubleBuffered = true;
        }

        public void Load(List<Image> imgs)
        {
            _imgWrappers = new List<ImageWrapper>();
            foreach (var img in imgs)
                _imgWrappers.Add(new ImageWrapper(img));         
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
            this.Cursor = GetHotItem(hitPoint) == null ? Cursors.Default : Cursors.Hand;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var hitPoint = e.Location.ChangePosition(0, _offsetY);
                var hotItem = GetHotItem(hitPoint);
                if (hotItem != null && OnItemClicked != null)
                    OnItemClicked(
                        this,
                        new ImageGridItemClickedEventArgs()
                        {
                            Image = hotItem.Image,
                            Index = _imgWrappers.IndexOf(hotItem)
                        });
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
        private ImageWrapper GetHotItem(Point location)
        {
            foreach (var i in _imgWrappers)
            {
                if (i.Boundary.Contains(location))
                {
                    return i;
                }
            }
            return null;
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