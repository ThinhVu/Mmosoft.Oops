using IP.Core.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    [Serializable]
    public partial class SingleLevelNavBar : Control
    {
        private bool _enableAcrylicStyle;
        [Browsable(true)]
        [Description("Enable/disable Acrylic visual style")]
        public bool EnableAcrylicStyle
        {
            get { return _enableAcrylicStyle; }
            set { _enableAcrylicStyle = value; MakeAcrylicBackground(); }
        }

        private bool _enableHighlightReveal;
        [Browsable(true)]
        [Description("Enable/disable highlight reveal effect")]
        public bool EnableHighlightReveal
        {
            get { return _enableHighlightReveal; }
            set { _enableHighlightReveal = value; }
        }

        private Color _clickedItem;        
        [Browsable(true)]
        [Description("Left side border color of clicked navigation bar item")]
        public Color ClickedItem
        {
            get { return _clickedItem; }
            set { _clickedItem = value; _navClickedItemBrush.Color = value; Invalidate(); }
        }

        // 
        private IP.ImageProcessor _imageProcessor;

        // 
        private List<NavBarItemWrapper> _navBarItems;

        // UI Configuration
        private const int ITEM_HEIGHT = 40;
        private const int ITEM_ICON_SIZE = 16;
        private const int ITEM_ICON_PADDING = 12;
        
        // dragging stuff
        private Point _mouseLocation;

        // Resource
        private SolidBrush _navBackgroundBrush;
        private SolidBrush _navItemBackgroundBrush;
        private SolidBrush _navClickedItemBrush;
        private SolidBrush _navItemTextBrush;        
        private Pen _navItemBorderPen;
        // reveal effect
        private LinearGradientBrush _navLeftRevealHighlightBrush;
        private LinearGradientBrush _navRightRevealHighlightBrush;

        //
        public SingleLevelNavBar()
        {
            _enableAcrylicStyle = false;
            _enableHighlightReveal = false;

            _imageProcessor = new IP.ImageProcessor();
            _imageProcessor.Filters.Add(new BlurFilter());
            //
            _navBarItems = new List<NavBarItemWrapper>();
            //
            _navBackgroundBrush = BrushCreator.CreateSolidBrush();
            _navItemBackgroundBrush = BrushCreator.CreateSolidBrush();
            _navClickedItemBrush = BrushCreator.CreateSolidBrush();
            _navItemTextBrush = BrushCreator.CreateSolidBrush("255, 0, 0, 0");
            _navItemBorderPen = PenCreator.Create();            
            //
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        /// <summary>
        /// Initial nav bar item wrapper collection
        /// </summary>
        /// <param name="sidebarItems"></param>
        public void Initialize(params NavBarItem[] sidebarItems)
        {
            this._navBarItems = new List<NavBarItemWrapper>();
            foreach (var item in sidebarItems)
                this._navBarItems.Add(new NavBarItemWrapper(item));

            UpdatePosition();
        }
        public void MakeAcrylicBackground()
        {
            if (EnableAcrylicStyle)
            {
                // no visual
                if (this.Parent == null) return;

                // to make an acrylic background
                // what we do is:
                // 1. find the form which host this nav bar
                // 2. move it some where off the screen
                // 3. capture screen picture at nav bar rect
                // 4. move the form to its old position
                // 5. apply blur effect to captured image
                var hostForm = FindForm();
                if (hostForm.WindowState == FormWindowState.Minimized)
                    return;
                // get position before moving host form to the most left
                var position = PointToScreen(Point.Empty);

                var left = hostForm.Left;
                hostForm.Left = int.MinValue;
                
                BackgroundImage = new Bitmap(this.Width, this.Height);
                using (var g = Graphics.FromImage(this.BackgroundImage))
                {
                    g.CopyFromScreen(position, Point.Empty, this.Size);
                    _imageProcessor.Process((Bitmap)this.BackgroundImage);
                }
                
                hostForm.Left = left;
                Invalidate();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);            
            MakeAcrylicBackground();
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
            
            Cursor = IsMouseHoverOverNavBarItem(e.Location) ? Cursors.Hand : Cursors.Default;

            foreach (var item in this._navBarItems)
                item.IsHovered = item.Boundary.Contains(e.Location);

            Invalidate();
        }        
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseLocation = new Point(int.MinValue, int.MinValue);
            
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
                UpdateItemPosition(item, ref x, ref y);
            }
        }
        private void UpdateItemPosition(NavBarItemWrapper item, ref int x, ref int y)
        {
            item.Boundary = new Rectangle(x, y, Width - x - 1, ITEM_HEIGHT);
            item.IconBoundary = new Rectangle(x + ITEM_ICON_PADDING, y + ITEM_ICON_PADDING, ITEM_ICON_SIZE, ITEM_ICON_SIZE);
            item.TextPosition = new Point(x + ITEM_ICON_PADDING * 2 + ITEM_ICON_SIZE, y + (ITEM_HEIGHT - TextRenderer.MeasureText(item.Text, Font).Height) / 2);
            y += ITEM_HEIGHT;
        }
        private void PaintItem(NavBarItemWrapper item, Graphics g)
        {
            if (item.IsHovered)
            {
                _navItemBackgroundBrush.Color = ExColorTranslator.Get("128, 128, 128, 128");
                _navItemBorderPen.Color = ExColorTranslator.Get("128, 93, 93, 93");
                
                g.FillRectangle(_navItemBackgroundBrush, item.Boundary);
                g.DrawRectangle(_navItemBorderPen, new Rectangle(-1, item.Boundary.Y, this.Width, item.Boundary.Height));
            }

            if (item.IsClicked)
            {                
                g.FillRectangle(_navClickedItemBrush, new Rectangle(0, item.Boundary.Y + 1, 3/*px*/, item.Boundary.Height - 1));
            }

            // Draw reveal highlight
            if (item.IsHovered)
            {
                if (_enableHighlightReveal)
                {
                    int halfRebelSize = 100;
                    // TODO:
                    // Brush created everytime so render performance will be decreased
                    // Replace with Translate matrix stuff
                    if (_navLeftRevealHighlightBrush != null)
                    {
                        _navLeftRevealHighlightBrush.Dispose();
                        _navLeftRevealHighlightBrush = null;
                    }

                    if (_navRightRevealHighlightBrush != null)
                    {
                        _navRightRevealHighlightBrush.Dispose();
                        _navRightRevealHighlightBrush = null;
                    }

                    Rectangle leftRevealHighlightRect = new Rectangle(_mouseLocation.X - halfRebelSize, item.Boundary.Y, halfRebelSize, item.Boundary.Height + 1);
                    Rectangle rightRevealHighlightRect = new Rectangle(_mouseLocation.X, item.Boundary.Y, halfRebelSize, item.Boundary.Height + 1);
                    _navLeftRevealHighlightBrush = new LinearGradientBrush(
                        leftRevealHighlightRect,
                        ExColorTranslator.Get("0, 200, 200, 200"),
                        ExColorTranslator.Get("100, 255, 255, 255"), 0f);
                    _navRightRevealHighlightBrush = new LinearGradientBrush(
                        rightRevealHighlightRect,
                        ExColorTranslator.Get("100, 255, 255, 255"),
                        ExColorTranslator.Get("0, 200, 200, 200"), 0f);
                    g.FillRectangle(_navLeftRevealHighlightBrush, leftRevealHighlightRect);
                    g.FillRectangle(_navRightRevealHighlightBrush, rightRevealHighlightRect);
                }                
            }

            if (item.Icon != null)
                g.DrawImage(item.Icon, item.IconBoundary);
            g.DrawString(item.Text, Font, _navItemTextBrush, item.TextPosition);
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
                    g.DrawImage(BackgroundImage, this.ClientRectangle);
                    _navBackgroundBrush.Color = ExColorTranslator.Get("200, 200, 200, 200");
                    g.FillRectangle(_navBackgroundBrush, ClientRectangle);
                }
                else
                {
                    _navBackgroundBrush.Color = BackColor;
                    g.FillRectangle(_navBackgroundBrush, ClientRectangle);
                }

                // paint item
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
                _navBackgroundBrush.Dispose();
                _navItemBackgroundBrush.Dispose();
                _navClickedItemBrush.Dispose();
                _navItemTextBrush.Dispose();

                if (_navLeftRevealHighlightBrush != null)
                    _navLeftRevealHighlightBrush.Dispose();
                if (_navRightRevealHighlightBrush != null)
                    _navRightRevealHighlightBrush.Dispose();

                _navItemBorderPen.Dispose();
            }
        }
    }    
}