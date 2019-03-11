using IP.Core.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    [Serializable]
    public partial class SingleLevelNavBar : Control
    {
        // 
        private IP.ImageProcessor _imageProcessor;

        // 
        private List<NavBarItemWrapper> _navBarItems;

        // UI Configuration
        private int _itemHeight = 40;
        private int _iconPadding = 12;
        private int _iconSize = 16;       
        private Point _mouseLocation;

        // Resource
        private SolidBrush _navBarbackgroundBrush;
        private SolidBrush _itemBackgroundBrush;
        private SolidBrush _clickedItemBrush;
        private SolidBrush _itemTextBrush;
        private LinearGradientBrush _leftRevealHighlightBrush;
        private LinearGradientBrush _rightRevealHighlightBrush;
        private Pen _itemBorderPen;

        //
        public SingleLevelNavBar()
        {
            _imageProcessor = new IP.ImageProcessor();
            _imageProcessor.Filters.Add(new BlurFilter());
            //
            _navBarItems = new List<NavBarItemWrapper>();
            //
            _navBarbackgroundBrush = BrushCreator.CreateSolidBrush();
            _itemBackgroundBrush = BrushCreator.CreateSolidBrush();
            _clickedItemBrush = BrushCreator.CreateSolidBrush();
            _itemTextBrush = BrushCreator.CreateSolidBrush("255, 0, 0, 0");
            _itemBorderPen = PenCreator.Create();            
            //
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        public void Initialize(params NavBarItem[] sidebarItems)
        {
            this._navBarItems = new List<NavBarItemWrapper>();
            foreach (var item in sidebarItems)
            {
                this._navBarItems.Add(new NavBarItemWrapper(item));
            }
            UpdatePosition();
        }        
        public void MakeAcrylicBackground()
        {
            if (this.Parent == null) return;

            var left = this.Parent.Left;
            var absPosOfNavbarWithScreen = PointToScreen(Point.Empty);
            var hostForm = FindForm();
            hostForm.Left = int.MinValue;
            BackgroundImage = new Bitmap(this.Width, this.Height);
            using (var graphic = Graphics.FromImage(this.BackgroundImage))
            {
                graphic.CopyFromScreen(absPosOfNavbarWithScreen, Point.Empty, this.Size);
                _imageProcessor.Process((Bitmap)this.BackgroundImage);
            }
            hostForm.Left = left;
            Invalidate();
        }
        
        // mouse stuff
        protected override void OnMouseClick(MouseEventArgs e)
        {            
            foreach (var item in _navBarItems)
            {
                if (item.Boundary.Contains(e.Location))
                {
                    item.IsClicked = true;
                    if (item.Clicked != null)
                        item.Clicked(item, e);
                }
                else
                {
                    item.IsClicked = false;
                }
            }
                
            Invalidate();
        }        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            _mouseLocation = e.Location;
            Cursor = IsMouseHoverOverNavBarItem(e.Location)? Cursors.Hand : Cursors.Default;
            foreach (var item in this._navBarItems)
                item.IsHovered = item.Boundary.Contains(e.Location);

            Invalidate();
        }        
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseLocation = new Point(-1000, -1000);
            Cursor = Cursors.Default;
            foreach (var item in this._navBarItems)
                item.IsHovered = false;            
            Invalidate();
        }
        private bool IsMouseHoverOverNavBarItem(Point location)
        {
            foreach (var item in _navBarItems)
            {
                if (item.Boundary.Contains(location))
                    return true;
            }
            return false;
        }

        // Paint stuff
        private void UpdatePosition()
        {
            int x = 0;
            int y = 0;
            foreach (var item in this._navBarItems)
            {
                UpdatePosition(item, ref x, ref y);
            }
        }
        private void UpdatePosition(NavBarItemWrapper item, ref int x, ref int y)
        {
            item.Boundary = new Rectangle(x, y, Width - x - 1, _itemHeight);
            item.IconBoundary = new Rectangle(x + _iconPadding, y + _iconPadding, _iconSize, _iconSize);
            item.TextPosition = new Point(x + _iconPadding * 2 + _iconSize, y + (_itemHeight - TextRenderer.MeasureText(item.Text, Font).Height) / 2);
            y += _itemHeight;
        }
        private void PaintItem(NavBarItemWrapper item, Graphics g)
        {
            if (item.IsHovered)
            {
                _itemBackgroundBrush.Color = ExColorTranslator.Get("128, 128, 128, 128");
                _itemBorderPen.Color = ExColorTranslator.Get("128, 93, 93, 93");
                
                g.FillRectangle(_itemBackgroundBrush, item.Boundary);
                g.DrawRectangle(_itemBorderPen, new Rectangle(-1, item.Boundary.Y, this.Width, item.Boundary.Height));
            }

            if (item.IsClicked)
            {
                if (_clickedItemBrush.Color != this.BackColor)
                    _clickedItemBrush.Color = this.BackColor;
                g.FillRectangle(_clickedItemBrush, new Rectangle(0, item.Boundary.Y + 1, 3/*px*/, item.Boundary.Height - 1));
            }

            if (item.Icon != null)
                g.DrawImage(item.Icon, item.IconBoundary);

            g.DrawString(item.Text, Font, _itemTextBrush, item.TextPosition);

            // Draw reveal highlight
            if (item.IsHovered)
            {
                int halfRebelSize = 100;
                // TODO:
                // Brush created everytime so render performance will be decreased
                // Replace with Translate matrix stuff
                if (_leftRevealHighlightBrush != null)
                {
                    _leftRevealHighlightBrush.Dispose();
                    _leftRevealHighlightBrush = null;
                }
                if (_rightRevealHighlightBrush != null)
                {
                    _rightRevealHighlightBrush.Dispose();
                    _rightRevealHighlightBrush = null;
                }
                Rectangle leftRevealHighlightRect = new Rectangle(_mouseLocation.X - halfRebelSize, item.Boundary.Y, halfRebelSize, item.Boundary.Height + 1);
                Rectangle rightRevealHighlightRect = new Rectangle(_mouseLocation.X, item.Boundary.Y, halfRebelSize, item.Boundary.Height + 1);
                _leftRevealHighlightBrush = new LinearGradientBrush(
                    leftRevealHighlightRect,
                    ExColorTranslator.Get("0, 200, 200, 200"),
                    ExColorTranslator.Get("100, 255, 255, 255"), 0f);
                _rightRevealHighlightBrush = new LinearGradientBrush(
                    rightRevealHighlightRect,
                    ExColorTranslator.Get("100, 255, 255, 255"),
                    ExColorTranslator.Get("0, 200, 200, 200"), 0f);
                g.FillRectangle(_leftRevealHighlightBrush, leftRevealHighlightRect);
                g.FillRectangle(_rightRevealHighlightBrush, rightRevealHighlightRect);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {            
            var g = e.Graphics;

            if (DesignMode)
            {
                g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            }
            else
            {
                if (BackgroundImage != null)
                {
                    g.DrawImage(BackgroundImage, Point.Empty);
                    _navBarbackgroundBrush.Color = ExColorTranslator.Get("200, 200, 200, 200");
                    g.FillRectangle(_navBarbackgroundBrush, ClientRectangle);
                }
                else
                {
                    _navBarbackgroundBrush.Color = BackColor;
                    g.FillRectangle(_navBarbackgroundBrush, ClientRectangle);
                }

                foreach (var item in _navBarItems)
                    PaintItem(item, g);
            }
        }        

        // Dispose
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _navBarbackgroundBrush.Dispose();
                _itemBackgroundBrush.Dispose();
                _clickedItemBrush.Dispose();
                _itemTextBrush.Dispose();
                _leftRevealHighlightBrush.Dispose();
                _rightRevealHighlightBrush.Dispose();
                _itemBorderPen.Dispose();
            }
        }
    }    
}
