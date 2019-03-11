using IP.Core.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    // TODO: Resource (pen, brush) management
    [Serializable]
    public partial class SingleLevelNavBar : Control
    {
        private IP.ImageProcessor _imageProcessor;
        private List<NavBarItemWrapper> items { get; set; }

        // UI Configuration
        public bool MultiLevel = false;
        public int ItemHeight = 40;
        public int IdentWidth = 20;
        public int IconPadding = 12;
        public int IconSize = 16;
        public int TextPadding = 40;
        public int DropDownSize = 6;
        public Point _mouseLocation;

        // Resource
        private SolidBrush _navBarbackgroundBrush;
        private SolidBrush _itemBackgroundBrush;
        private SolidBrush _clickedItemBrush;
        private SolidBrush _itemTextBrush;
        private LinearGradientBrush _leftRevealHighlightBrush;
        private LinearGradientBrush _rightRevealHighlightBrush;
        private Pen _itemBorderPen;

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

        public SingleLevelNavBar()
        {
            _imageProcessor = new IP.ImageProcessor();
            _imageProcessor.Filters.Add(new BlurFilter());

            items = new List<NavBarItemWrapper>();

            _navBarbackgroundBrush = BrushCreator.CreateSolidBrush();
            _itemBackgroundBrush = BrushCreator.CreateSolidBrush();
            _clickedItemBrush = BrushCreator.CreateSolidBrush();
            _itemTextBrush = BrushCreator.CreateSolidBrush("255, 0, 0, 0");
            _itemBorderPen = PenCreator.Create();            

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //
        public void MakeAcrylicBackground()
        {
            var left = this.Parent.Left;
            var point = PointToScreen(Point.Empty);

            this.Parent.Left = int.MinValue;

            if (this.BackgroundImage == null)
            {
                this.BackgroundImage = new Bitmap(this.Width, this.Height);
            }

            using (Graphics g = Graphics.FromImage(this.BackgroundImage))
            {
                g.CopyFromScreen(point, Point.Empty, this.Size);
                _imageProcessor.Process((Bitmap)this.BackgroundImage);
            }

            this.Parent.Left = left;
            
            Invalidate();
        }
        
        protected override void OnMouseClick(MouseEventArgs e)
        {            
            foreach (var item in items)
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
            foreach (var item in this.items)
                item.IsHovered = item.Boundary.Contains(e.Location);

            Invalidate();
        }
        private bool IsMouseHoverOverNavBarItem(Point location)
        {
            foreach (var item in items)
            {
                if (item.Boundary.Contains(location))
                    return true;
            }
            return false;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseLocation = new Point(-1000, -1000);
            Cursor = Cursors.Default;
            foreach (var item in this.items)
                item.IsHovered = false;            
            Invalidate();
        }

        // Paint stuff
        protected override void OnPaint(PaintEventArgs e)
        {            
            var g = e.Graphics;

            if (DesignMode)
            {
                g.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
            }
            else
            {
                if (this.BackgroundImage != null)
                {
                    g.DrawImage(this.BackgroundImage, Point.Empty);
                    _navBarbackgroundBrush.Color = CustomColorTranslator.Get("200, 200, 200, 200");
                    g.FillRectangle(_navBarbackgroundBrush, this.ClientRectangle);
                }
                else
                {
                    _navBarbackgroundBrush.Color = this.BackColor;
                    g.FillRectangle(_navBarbackgroundBrush, this.ClientRectangle);
                }

                foreach (var item in this.items)
                    DrawItem(item, g);
            }
        }
        private void DrawItem(NavBarItemWrapper item, Graphics g)
        {        
            if (item.IsHovered)
            {
                _itemBackgroundBrush.Color = CustomColorTranslator.Get("128, 128, 128, 128");                               
                _itemBorderPen.Color = CustomColorTranslator.Get("128, 93, 93, 93");
                // 
                g.FillRectangle(_itemBackgroundBrush, item.Boundary);
                g.DrawRectangle(_itemBorderPen, new Rectangle(-1, item.Boundary.Y, this.Width, item.Boundary.Height));
            }

            if (item.IsClicked)
            {
                if (_clickedItemBrush.Color != this.BackColor)
                    _clickedItemBrush.Color = this.BackColor;
                g.FillRectangle(_clickedItemBrush, new Rectangle(0, item.Boundary.Y + 1, 3, item.Boundary.Height - 1));
            }

            if (item.Icon != null)
                g.DrawImage(item.Icon, item.IconBoundary);
           
            g.DrawString(item.Text, this.Font, _itemTextBrush, item.TextPosition);

            // Draw rebel
            if (item.IsHovered)
            {
                int halfRebelSize = 100;
                // TODO: Create everytime reduce render performance
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
                    CustomColorTranslator.Get("0, 200, 200, 200"),
                    CustomColorTranslator.Get("100, 255, 255, 255"), 0f);
                _rightRevealHighlightBrush = new LinearGradientBrush(
                    rightRevealHighlightRect,
                    CustomColorTranslator.Get("100, 255, 255, 255"),
                    CustomColorTranslator.Get("0, 200, 200, 200"), 0f);
                g.FillRectangle(_leftRevealHighlightBrush, leftRevealHighlightRect);
                g.FillRectangle(_rightRevealHighlightBrush, rightRevealHighlightRect);
            }
        }

        public void Initialize(params NavBarItem[] sidebarItems)
        {
            this.items = new List<NavBarItemWrapper>();
            foreach (var item in sidebarItems)
            {
                this.items.Add(new NavBarItemWrapper(item));
            }
            CalculatePosition();
        }
        private void CalculatePosition()
        {
            int x = 0;
            int y = 0;
            foreach (var item in this.items)
            {
                CalculatePosition(item, ref x, ref y);
            }
        }
        private void CalculatePosition(NavBarItemWrapper item, ref int x, ref int y)
        {
            item.Boundary = new Rectangle(x, y, this.Width - x - 1, this.ItemHeight);
            item.IconBoundary = new Rectangle(x + IconPadding, y + IconPadding, IconSize, IconSize);
            item.TextPosition = new Point(x + TextPadding, y + (this.ItemHeight - TextRenderer.MeasureText(item.Text, this.Font).Height) / 2);
            y += this.ItemHeight;
        }
    }    
}
