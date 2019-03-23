using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops
{
    [Serializable]
    public partial class MultiLevelNavBar : Control
    {
        private bool _enableHighlightReveal;
        [Browsable(true)]
        [Description("Enable/disable highlight reveal effect")]
        public bool EnableHighlightReveal
        {
            get { return _enableHighlightReveal; }
            set { _enableHighlightReveal = value; }
        }

        private List<NavBarItemWrapper> _navBarItems { get; set; }

        // UI Configuration
        private const int ITEM_HEIGHT = 40;
        private const int ITEM_ICON_SIZE = 16;
        private const int ITEM_ICON_PADDING = 12;
        private const int IDEN_WIDTH = 3;
        private const int DROP_DOWN_PADDING = 17;
        private const int DROP_DOWN_SIZE = 6;

        // UI remember
        private HitTestItem lastHitTestItem;

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

        public MultiLevelNavBar()
        {
            _enableHighlightReveal = false;

            _navBarItems = new List<NavBarItemWrapper>();

            _navBackgroundBrush = BrushCreator.CreateSolidBrush();
            _navItemBackgroundBrush = BrushCreator.CreateSolidBrush();
            _navClickedItemBrush = BrushCreator.CreateSolidBrush();
            _navItemTextBrush = BrushCreator.CreateSolidBrush("255, 0, 0, 0");
            _navItemBorderPen = PenCreator.Create();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);            
        }

        public void Initialize(params NavBarItem[] sidebarItems)
        {
            this._navBarItems = new List<NavBarItemWrapper>();
            foreach (var item in sidebarItems)
                this._navBarItems.Add(new NavBarItemWrapper(item));

            UpdatePosition();
        }
                
        protected override void OnMouseClick(MouseEventArgs e)
        {
            var hitTest = HitTest(this._navBarItems, e.Location);
            if (hitTest != null)
            {
                if (hitTest.Action == HitTestAction.DropDownClicked)
                {
                    if (lastHitTestItem != null)
                        lastHitTestItem.Item.IsClicked = false;
                    hitTest.Item.IsClicked = true;
                    hitTest.Item.IsExpanded = !hitTest.Item.IsExpanded;
                    UpdatePosition();
                    if (hitTest.Item.Clicked != null)
                        hitTest.Item.Clicked(_navBarItems, e);
                }
                else
                {
                    if (lastHitTestItem != null)
                    {
                        lastHitTestItem.Item.IsClicked = false;
                        // do click
                        if (hitTest.Item != lastHitTestItem.Item)
                            if (hitTest.Item.Clicked != null)
                                hitTest.Item.Clicked(_navBarItems, e);
                    }
                    else
                    {
                        if (hitTest.Item.Clicked != null)
                            hitTest.Item.Clicked(_navBarItems, e);
                    }

                    hitTest.Item.IsClicked = true;
                }

                lastHitTestItem = hitTest;
            }
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            _mouseLocation = e.Location;
            
            Cursor = HitTest(this._navBarItems, e.Location) != null ? Cursors.Hand : Cursors.Default;
            UpdateHoverState(this._navBarItems, e.Location);
            
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseLocation = new Point(int.MinValue, int.MinValue);

            Cursor = Cursors.Default;

            ClearHover(_navBarItems);
            
            Invalidate();
        }

        private void ClearHover(List<NavBarItemWrapper> items)
        {
            foreach (var item in items)
            {
                item.IsHovered = false;
                if (ItemHasChild(item))
                    ClearHover(item.Items);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
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
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            if (DesignMode)
            {
                g.DrawRectangle(Pens.Black, this.ClientRectangle.ChangeSizeRelative(-1, -1));
                g.DrawString(this.Name + " control doesn't provide design time support", this.Font, Brushes.Black, new PointF(0, 0));
            }
            else
            {
                foreach (var item in this._navBarItems)
                    PaintItem(item, g);
            }
        }

        private void UpdateHoverState(List<NavBarItemWrapper> items, Point location)
        {
            foreach (var item in items)
            {
                item.IsHovered = item.Boundary.Contains(location);
                if (ItemHasChild(item))
                {
                    UpdateHoverState(item.Items, location);
                }
            }
        }
        private bool ItemHasChild(NavBarItemWrapper item)
        {
            return item.Items != null && item.Items.Count > 0;
        }
        private HitTestItem HitTest(List<NavBarItemWrapper> items, Point hit)
        {
            HitTestItem hitTest = null;

            foreach (var item in items)
            {
                if (item.Boundary.Contains(hit))
                {
                    hitTest = new HitTestItem
                    {
                        Item = item,
                        Action = ItemHasChild(item) ? HitTestAction.DropDownClicked : HitTestAction.ItemClicked
                    };
                }
                else // check child
                {
                    if (ItemHasChild(item))
                    {
                        hitTest = HitTest(item.Items, hit);
                    }
                }

                // if hitTest found, return
                if (hitTest != null)
                    return hitTest;
            }
            return hitTest;
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
            
            // draw arrow and child item if needed
            if (item.Items != null)
            {
                if (item.IsExpanded)
                {
                    g.DrawImage(SvgPath8x8Mgr.Get("M0,0H8L4,8z", 1, Brushes.White), item.DropDownButtonBoundary);
                    foreach (var childItem in item.Items)
                    {
                        PaintItem(childItem, g);
                    }
                }
                else
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    g.DrawImage(SvgPath8x8Mgr.Get("M0,8H8L4,0z", 1, Brushes.White), item.DropDownButtonBoundary);
                }
            }                              
        }        
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
            item.Boundary = new Rectangle(x, y, Width - x - 1, ITEM_HEIGHT);
            item.DropDownButtonBoundary = new Rectangle(item.Boundary.Right - DROP_DOWN_PADDING - DROP_DOWN_SIZE, item.Boundary.Top + DROP_DOWN_PADDING, DROP_DOWN_SIZE, DROP_DOWN_SIZE);
            item.IconBoundary = new Rectangle(x + ITEM_ICON_PADDING, y + ITEM_ICON_PADDING, ITEM_ICON_SIZE, ITEM_ICON_SIZE);
            item.TextPosition = new Point(x + ITEM_ICON_PADDING * 2 + ITEM_ICON_SIZE, y + (ITEM_HEIGHT - TextRenderer.MeasureText(item.Text, Font).Height) / 2);
            y += ITEM_HEIGHT;

            if (ItemHasChild(item))
            {                
                if (item.IsExpanded)
                {
                    x += IDEN_WIDTH;
                    foreach (var childItem in item.Items)
                    {
                        UpdatePosition(childItem, ref x, ref y);
                    }
                    x -= IDEN_WIDTH;
                }
                else
                {
                    // Reset position -- prevent hit test
                    foreach (var childItem in item.Items)
                    {
                        childItem.Boundary = Rectangle.Empty;
                        childItem.DropDownButtonBoundary = Rectangle.Empty;
                        childItem.TextPosition = Point.Empty;
                    }
                }
            }
        }
    }    
}