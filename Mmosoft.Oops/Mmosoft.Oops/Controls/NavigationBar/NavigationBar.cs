using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    [Serializable]
    public partial class NavigationBar : Control
    {
        private List<NavBarItemWrapper> _navBarItems;
        private bool _enableHighlightReveal;
        // UI Configuration
        private int _itemHeight = 40;
        private int _itemIconSize = 16;
        private int _identWidth = 20;
        private int _dropdownSize = 6;
        
        [Browsable(true)]
        public int ItemHeight 
        {
            get
            {
                return _itemHeight;
            }
            set
            {
                if (value <= 0)
                    return;
                _itemHeight = value;
                UpdatePosition();
                Invalidate();
            }
        }
        [Browsable(true)]
        public int ItemIconSize 
        {
            get
            {
                return _itemIconSize;
            }
            set
            {
                if (value < 0 || value > _itemHeight)
                    return;
                _itemIconSize = value;
                UpdatePosition();
                Invalidate();
            }
        }
        [Browsable(true)]
        public int IdentWidth
        {
            get
            {
                return _identWidth;
            }
            set
            {
                if (value <= 0)
                    return;
                _identWidth = value;
                UpdatePosition();
                Invalidate();
            }
        }
        [Browsable(true)]
        public int DropDownSize
        {
            get
            {
                return _dropdownSize;
            }
            set
            {
                _dropdownSize = value;
                UpdatePosition();
                Invalidate();
            }
        }

        // UI remember
        private NavBarItemWrapper _lastHitTestItem;

        // 
        private NavBarItemWrapper _collapseExpandItem;
        private bool _collapseExpandEnable;
        private bool _collapsing;
        private int _expandedWidth;
        private Bitmap _collapsingImage;
        private Bitmap _expadingImage;


        // dragging stuff
        private Point _mouseLocation;
        private bool _isMouseIn;

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
        [Browsable(true)]
        public bool CollapseExpandEnable
        {
            get
            {
                return _collapseExpandEnable;
            }
            set
            {
                _collapseExpandEnable = value;
                UpdatePosition();
                Invalidate();
            }
        }
        [Browsable(true)]
        public bool EnableHighlightReveal
        {
            get { return _enableHighlightReveal; }
            set { _enableHighlightReveal = value; }
        }
        [Browsable(false)]
        public bool IsCollapsing { get { return _collapsing; } }
        [Browsable(false)]
        public int CollapsedWidth { get { return _itemHeight; } }
        [Browsable(false)]
        public int ExpanedWidth { get { return _expandedWidth; } }

        //
        public event EventHandler OnCollapseExpandStateChanged;

        //
        public NavigationBar()
        {
            _enableHighlightReveal = false;

            //
            _navBarItems = new List<NavBarItemWrapper>();

            //
            // add collapse button
            _collapsingImage = SvgPath8x8Mgr.Get("M5 1v2h-5v2h5v2l3-3.03-3-2.97z" /*=>*/, 4, Brushes.Black, SmoothingMode.HighQuality);
            _expadingImage = SvgPath8x8Mgr.Get("M3 1l-3 3.03 3 2.97v-2h5v-2h-5v-2z" /*<=*/, 4, Brushes.Black, SmoothingMode.HighQuality);
            _collapseExpandItem = new NavBarItemWrapper(new NavBarItem
            {
                Icon = _expadingImage,
                Clicked = (s, e) =>
                {
                    _collapsing = !_collapsing;
                    _collapseExpandItem.Icon = _collapsing ? _collapsingImage : _expadingImage;
                    this.Width = _collapsing ? _itemHeight : _expandedWidth;
                    UpdatePosition();
                    if (OnCollapseExpandStateChanged != null)
                        OnCollapseExpandStateChanged(this, EventArgs.Empty);
                }
            });
            _expandedWidth = 200;

            //
            _navBackgroundBrush = BrushCreator.CreateSolidBrush();
            _navItemBackgroundBrush = BrushCreator.CreateSolidBrush();
            _navClickedItemBrush = BrushCreator.CreateSolidBrush();
            _navItemTextBrush = BrushCreator.CreateSolidBrush("255, 0, 0, 0");
            _navItemBorderPen = PenCreator.Create();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);            
        }
        //
        public void Initialize(params NavBarItem[] navbarItems)
        {
            this._navBarItems = new List<NavBarItemWrapper>();
            // add sidebar items
            foreach (var item in navbarItems)
                this._navBarItems.Add(new NavBarItemWrapper(item));
            UpdatePosition();
        }

        //
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            HandleMouseClick(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _mouseLocation = e.Location;
            _isMouseIn = true;

            Cursor = GetClickedItem(this._navBarItems, e.Location) != null ? Cursors.Hand : Cursors.Default;
            UpdateHoverState(this._navBarItems, e.Location);
            
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseLocation = new Point(int.MinValue, int.MinValue);
            _isMouseIn = false;

            Cursor = Cursors.Default;

            ClearHover(_navBarItems);
            
            Invalidate();
        }

        //
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_collapsing)
                _expandedWidth = this.Width;
        }

        //
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
            base.OnPaint(e);
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
        
        //
        private void HandleMouseClick(MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            var clickedItem = GetClickedItem(this._navBarItems, e.Location);
            if (clickedItem != null)
            {
                if (clickedItem == _collapseExpandItem)
                {
                    _collapseExpandItem.Clicked(this, e);
                }
                else
                {
                    if (_lastHitTestItem != null)
                        _lastHitTestItem.IsClicked = false;

                    clickedItem.IsExpanded = ItemHasChild(clickedItem) && !clickedItem.IsExpanded;
                    clickedItem.IsClicked = true;
                    _lastHitTestItem = clickedItem;
                    UpdatePosition();
                    if (clickedItem.Clicked != null)
                        clickedItem.Clicked(_navBarItems, e);
                }
            }
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
        private NavBarItemWrapper GetClickedItem(List<NavBarItemWrapper> items, Point hit)
        {
            NavBarItemWrapper hitTest = null;

            foreach (var item in items)
            {
                if (item.Boundary.Contains(hit))
                {
                    hitTest = item;
                }
                else // check child
                {
                    if (ItemHasChild(item))
                    {
                        hitTest = GetClickedItem(item.Items, hit);
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
            if (item != _collapseExpandItem && (item.IsHovered || item.IsClicked))
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
            if (item.IsHovered && item != _collapseExpandItem)
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

            if (item != _collapseExpandItem)
                g.DrawString(item.Text, Font, _navItemTextBrush, item.TextPosition);
            
            // draw arrow and child item if needed
            if (item != _collapseExpandItem && item.Items != null)
            {
                if (item.IsExpanded)
                {
                    // Draw dropdown arrow only mouse enter nav bar
                    if (_isMouseIn && !_collapsing)
                        g.DrawImage(SvgPath8x8Mgr.Get("M0,0H8L4,8z", 1, Brushes.Black), item.DropDownButtonBoundary);
                    foreach (var childItem in item.Items)
                        PaintItem(childItem, g);
                }
                else
                {
                    // Draw dropdown arrow only mouse enter nav bar
                    if (_isMouseIn && !_collapsing)
                        g.DrawImage(SvgPath8x8Mgr.Get("M0,8H8L4,0z", 1, Brushes.Black), item.DropDownButtonBoundary);
                }
            }
        }        
        private void UpdatePosition()
        {
            if (!_collapseExpandEnable)
            {
                if (_navBarItems != null
                    && _navBarItems.Count > 0
                    && _navBarItems[0] == _collapseExpandItem)
                {
                    _navBarItems.RemoveAt(0);
                }
            }
            else
            {
                if (_navBarItems != null && _navBarItems[0] != _collapseExpandItem)
                    _navBarItems.Insert(0, _collapseExpandItem);
            }

            int y = 0;
            foreach (var item in this._navBarItems)
                UpdatePosition(item, ref y);
        }
        private void UpdatePosition(NavBarItemWrapper item, ref int y, int ident = 0)
        {
            int itemIconPadding = (_itemHeight - _itemIconSize) / 2;
            int dropdownPadding = (_itemHeight - _dropdownSize) / 2;
            int actualIdent = _collapsing ? 0 : ident;
            item.IdentPixel = actualIdent;
            item.Boundary = new Rectangle(0, y, Width - 1, _itemHeight);
            item.DropDownButtonBoundary = new Rectangle(item.Boundary.Right - dropdownPadding - _dropdownSize, item.Boundary.Top + dropdownPadding, _dropdownSize, _dropdownSize);
            item.IconBoundary = new Rectangle(itemIconPadding + actualIdent, y + itemIconPadding, _itemIconSize, _itemIconSize);
            item.TextPosition = new Point(itemIconPadding * 2 + _itemIconSize + actualIdent, y + (_itemHeight - TextRenderer.MeasureText(item.Text, Font).Height) / 2);
            y += _itemHeight;

            if (ItemHasChild(item))
            {
                if (item.IsExpanded)
                {
                    foreach (var childItem in item.Items)
                    {
                        UpdatePosition(childItem, ref y, actualIdent + _identWidth);
                    }
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