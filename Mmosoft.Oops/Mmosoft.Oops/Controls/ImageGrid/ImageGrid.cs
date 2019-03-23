using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            if (_imgWrappers != null)
            {
                foreach (ImageWrapper imgW in _imgWrappers)
                {
                    g.DrawImage(imgW.Image,
                        new Rectangle(
                            imgW.Boundary.X,
                            imgW.Boundary.Y - _offsetY,
                            imgW.Boundary.Width,
                            imgW.Boundary.Height));
                }
            }
        }
    }
}