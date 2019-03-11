using IP.Core.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops
{
    [Serializable]
    public partial class NavBar : Control
    {
        IP.ImageProcessor _imageProcessor;

        // Item data
        private List<NavBarItemWrapper> items { get; set; }     

        // UI Configuration
        public bool MultiLevel = false;
        public int ItemHeight = 40;
        public int IdentWidth = 20;
        public int TextPadding = 10;
        public int DropDownSize = 6;

        // UI remember
        private NavBarItemWrapper hoveredItem;
        private HitTestItem lastHitTestItem;

        // for outside draw
        private Rectangle outSideBoundary;
        private Point outsidePoint;

        public NavBar()
        {
            _imageProcessor = new IP.ImageProcessor();
            _imageProcessor.Filters.Add(new BlurFilter());

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            items = new List<NavBarItemWrapper>();

            outSideBoundary = new Rectangle(-1, -1, 0, 0);
            outsidePoint = new Point(-1, -1);
        }

        //
        public void CaptureBackgroundImage()
        {
            var left = this.Parent.Left;
            var point = PointToScreen(Point.Empty);

            this.Parent.Left = -1000;

            if (this.BackgroundImage == null)
            {
                this.BackgroundImage = new Bitmap(this.Width, this.Height);
            }

            using (Graphics g = Graphics.FromImage(this.BackgroundImage))
            {
                g.CopyFromScreen(point, Point.Empty, this.Size);                
            }

            this.Parent.Left = left;
        }

        // Mouse stuff
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
        private NavBarItemWrapper DisplayHoverIfNeeded(List<NavBarItemWrapper> items, Point hit)
        {
            NavBarItemWrapper foundItem = null;
            foreach (var item in items)
            {
                if (item.Boundary.Contains(hit))
                {
                    item.IsHovered = true;
                    // found item, return immediately
                    foundItem = item;
                }
                else
                {
                    item.IsHovered = false;
                }
                // hovered item not found, find in child item
                if (item.Items != null && item.Items.Count > 0)
                {
                    var itemHo = DisplayHoverIfNeeded(item.Items, hit);
                    if (itemHo != null)
                        foundItem = itemHo;
                }
            }
            return foundItem;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var hitTest = HitTest(this.items, e.Location);
            if (hitTest != null)
            {
                if (hitTest.Action == HitTestAction.DropDownClicked)
                {
                    if (lastHitTestItem != null)
                        lastHitTestItem.Item.IsClicked = false;
                    hitTest.Item.IsClicked = true;
                    hitTest.Item.IsExpanded = !hitTest.Item.IsExpanded;
                    CalculatePosition();
                    if (hitTest.Item.Clicked != null)
                        hitTest.Item.Clicked(items, e);
                }
                else
                {
                    if (lastHitTestItem != null)
                    {
                        lastHitTestItem.Item.IsClicked = false;
                        // do click
                        if (hitTest.Item != lastHitTestItem.Item)
                            if (hitTest.Item.Clicked != null)
                                hitTest.Item.Clicked(items, e);
                    }
                    else
                    {
                        if (hitTest.Item.Clicked != null)
                            hitTest.Item.Clicked(items, e);
                    }

                    hitTest.Item.IsClicked = true;
                }

                lastHitTestItem = hitTest;
            }
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = Cursors.Hand;
            var hoveredItem = DisplayHoverIfNeeded(this.items, e.Location);
            if (hoveredItem != null && hoveredItem != this.hoveredItem)
            {
                this.hoveredItem = hoveredItem;
                Invalidate();
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
            this.hoveredItem = null;
            DisplayHoverIfNeeded(this.items, new Point(-1, -1));
            Invalidate();
        }

        // Paint stuff
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // g.FillRectangle(BrushCreator.CreateSolidBrush(NavBarColors.SideBarColors.Bg), ClientRectangle);
            if (this.BackgroundImage != null)
            {
                var processedBackgroundImage = (Bitmap)this.BackgroundImage.Clone();
                _imageProcessor.Process(processedBackgroundImage);
                g.DrawImage(processedBackgroundImage, Point.Empty);
                g.FillRectangle(BrushCreator.CreateSolidBrush("128, 128, 128, 128"), this.ClientRectangle);            
            }

            if (!DesignMode)
            {
                foreach (var item in this.items)
                    DrawItem(item, g);
            }
        }
        private void DrawItem(NavBarItemWrapper item, Graphics g)
        {        
            string itemBg, itemText, itemArrow, itemBorder;           
            if (item.IsHovered)
            {
                itemBg = NavBarColors.SideBarItemColors.BgHovered;
                itemText = NavBarColors.SideBarItemColors.TextHovered;
                itemArrow = this.MultiLevel
                    ? NavBarColors.MultiLevelSidebarItemColors.ArrowHovered
                    : NavBarColors.SingleLevelSidebarColors.ArrowHovered;
                itemBorder = NavBarColors.SideBarItemColors.BorderHovered;
            }
            else if (item.IsClicked)
            {
                itemBg = NavBarColors.SideBarItemColors.BgFocused;
                itemText = NavBarColors.SideBarItemColors.TextFocused;
                itemArrow = this.MultiLevel
                    ? NavBarColors.MultiLevelSidebarItemColors.ArrowFocused
                    : NavBarColors.SingleLevelSidebarColors.ArrowFocused;
                itemBorder = NavBarColors.SideBarItemColors.BorderFocused;
            }
            else
            {
                itemBg = NavBarColors.SideBarItemColors.Bg;
                itemText = NavBarColors.SideBarItemColors.Text;
                itemArrow = this.MultiLevel
                    ? NavBarColors.MultiLevelSidebarItemColors.Arrow
                    : NavBarColors.SingleLevelSidebarColors.Arrow;
                itemBorder = NavBarColors.SideBarItemColors.Border;
            }

            if (item.IsHovered)
                g.FillRectangle(BrushCreator.CreateSolidBrush("128, 255, 255, 255"), item.Boundary);
            // TODO: draw with 1 or 2 pixel error (setting # border color to see)
            //g.DrawRectangle(PenCreator.Create(itemBorder, 1f), 
            //    new Rectangle(item.Boundary.X, item.Boundary.Y, item.Boundary.Width, item.Boundary.Height));
            g.DrawString(item.Text, this.Font, BrushCreator.CreateSolidBrush("255, 0, 0, 0"), item.TextPosition);

            // arrow & child
            if (this.MultiLevel)
            {
                // draw arrow and child item if needed
                if (item.Items != null)
                {
                    if (item.IsExpanded)
                    {                        
                        g.DrawImage(SvgPath8x8Mgr.Get("M0,0H8L4,8z", 1, BrushCreator.CreateSolidBrush(itemArrow)), item.DropDownButtonBoundary);
                        foreach (var childItem in item.Items)
                        {
                            DrawItem(childItem, g);
                        }
                    }
                    else
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                        g.DrawImage(SvgPath8x8Mgr.Get("M0,8H8L4,0z", 1, BrushCreator.CreateSolidBrush(itemArrow)), item.DropDownButtonBoundary);                        
                    }
                }
            }
            else if (item.IsClicked)
            {
                Rectangle leftArrowRect = new Rectangle();
                leftArrowRect.Height = item.Boundary.Height;
                leftArrowRect.Width = item.Boundary.Height / 2;
                leftArrowRect.X = item.Boundary.X + item.Boundary.Width - leftArrowRect.Width + 1;
                leftArrowRect.Y = item.Boundary.Y;
                g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowLeft, 10, BrushCreator.CreateSolidBrush(itemArrow)), leftArrowRect);
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
            item.DropDownButtonBoundary = new Rectangle(this.Width - 20, y + (this.ItemHeight - DropDownSize) / 2, DropDownSize, DropDownSize);
            item.TextPosition = new Point(x + TextPadding, y + (this.ItemHeight - TextRenderer.MeasureText(item.Text, this.Font).Height) / 2);
            y += this.ItemHeight;

            if (ItemHasChild(item))
            {
                this.MultiLevel = true;
                if (item.IsExpanded)
                {
                    x += this.IdentWidth; // iden
                    foreach (var childItem in item.Items)
                    {
                        CalculatePosition(childItem, ref x, ref y);
                    }
                    x -= this.IdentWidth;
                }
                else
                {
                    // Reset position -- prevent hit test
                    foreach (var childItem in item.Items)
                    {
                        childItem.Boundary = outSideBoundary;
                        childItem.DropDownButtonBoundary = outSideBoundary;
                        childItem.TextPosition = outsidePoint;
                    }
                }
            }
        }
    }    
}
