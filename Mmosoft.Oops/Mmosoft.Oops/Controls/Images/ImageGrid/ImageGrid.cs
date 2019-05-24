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
        private int _virtualHeight;
        private int _offsetY;
        // private data member <which has corresponding public property>
        private int _gutter;
        private int _column;
        private int _selectedIndex;
        private ImageGridDisplayMode _displayMode;
        private ImageGridFlow _flow;

        [Browsable(true)]
        [Description("Pixel between each image")]
        public int Gutter
        {
            get { return _gutter; }
            set { _gutter = value; }
        }
        
        [Browsable(true)]
        [Description("Number of column will be displayed in image grid")]
        public int Column
        {
            get { return _column; }
            set { _column = value; ReDraw(); }
        }

        [Browsable(true)]
        [Description("DisplayMode")]
        public ImageGridDisplayMode DisplayMode
        {
            get
            {
                return _displayMode;
            }
            set
            {
                if (_displayMode != value)
                {
                    _displayMode = value;
                    ReDraw();
                }
            }
        }

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

        // events
        public event ImageGridItemClickedEventHandler OnItemClicked;

        // methods
        public ImageGrid()
        {
            _column = 3;
            _gutter = 5;
            _imgWrappers = new List<ImageWrapper>();
            //
            DoubleBuffered = true;
        }

        //
        public void Clear()
        {
            foreach (var imageWrapper in _imgWrappers)
                imageWrapper.Image.Dispose();
            _imgWrappers = new List<ImageWrapper>();
            _offsetY = 0;
            ReDraw();
        }
        public void Add(Image image)
        {
            _imgWrappers.Add(new ImageWrapper(image));
            ReDraw();
        }
        public void ReDraw()
        {
            _virtualHeight = 0;
            if (_imgWrappers == null || _imgWrappers.Count == 0)
                return;
            switch (_flow)
            {
                case ImageGridFlow.FillToTop:
                    FillToTop();
                    break;
                case ImageGridFlow.RowByRow:
                    RowByRow();
                    break;
            }

            Invalidate();
        }

        private void FillToTop()
        {
            var availableWidth = (int)((this.Width - 1 - (_column + 1) * _gutter * 1f) / _column);

            var columnTops = new int[_column];
            for (int i = 0; i < columnTops.Length; i++)
                columnTops[i] = _gutter;

            int left, top, colId;
            for (int i = 0; i < _imgWrappers.Count; i++)
            {
                GetMinHeightAndColumnIndex(columnTops, out top, out colId);
                left = _gutter * (1 + colId) + availableWidth * colId;

                ImageWrapper iw = _imgWrappers[i];
                int actualImageWidth = iw.Image.Width;
                int actualImageHeight = iw.Image.Height;
                int availableHeight = (int)((availableWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.Boundary = new Rectangle(left, top - _offsetY, availableWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                columnTops[colId] += availableHeight + _gutter;

                // increase virtual height
                if (_virtualHeight < columnTops[colId])
                    _virtualHeight = columnTops[colId];
            }
        }

        private void RowByRow()
        {
            var availableWidth = (int)((this.Width - 1 - (_column + 1) * _gutter * 1f) / _column);
            var columnTops = new int[_column];
            for (int i = 0; i < columnTops.Length; i++)
                columnTops[i] = _gutter;
            int left, top, colId;
            for (int i = 0; i < _imgWrappers.Count; i++)
            {
                GetMinHeightAndColumnIndex(columnTops, out top, out colId);
                left = _gutter * (1 + colId) + availableWidth * colId;

                ImageWrapper iw = _imgWrappers[i];
                int actualImageWidth = iw.Image.Width;
                int actualImageHeight = iw.Image.Height;
                int availableHeight = (int)((availableWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.Boundary = new Rectangle(left, top - _offsetY, availableWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                columnTops[colId] += availableHeight + _gutter;

                // increase virtual height
                if (_virtualHeight < columnTops[colId])
                    _virtualHeight = columnTops[colId];
            }
            switch (DisplayMode)
            {
                case ImageGridDisplayMode.ScaleLossCenter:
                    break;
                case ImageGridDisplayMode.StretchImage:
                    break;
            }
        }

        //
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ReDraw();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = GetHotItemIndex(e.Location) < 0 ? Cursors.Default : Cursors.Hand;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.SelectedIndex = GetHotItemIndex(e.Location);
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

            ReDraw();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            SolidBrush br = new SolidBrush(this.BackColor);
            g.FillRectangle(br, this.ClientRectangle);
            if (DesignMode)
            {
                g.DrawString(this.Name + " control doesn't provide design time support", this.Font, Brushes.Black, new Point(0, 0));
            }
            else
            {
                foreach (ImageWrapper i in ImagesInView())
                {
                    g.DrawImage(i.Image, i.Boundary);
                }
            }

            br.Dispose();
        }
        
        private void GetMinHeightAndColumnIndex(int[] num, out int value, out int index)
        {
            value = int.MaxValue;
            index = -1;
            // looping from right to left
            // so left side will have higher priority
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
        private IEnumerable<ImageWrapper> ImagesInView()
        {
            foreach (var iw in _imgWrappers)
            {
                if (iw.Boundary.IntersectsWith(this.ClientRectangle))
                    yield return iw;
            }
        }
    }
}