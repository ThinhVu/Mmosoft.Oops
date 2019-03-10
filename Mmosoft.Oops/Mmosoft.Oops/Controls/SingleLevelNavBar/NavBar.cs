using IP.Core.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    // TODO: Resource (pen, brush) management
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
        public int IconPadding = 12;
        public int IconSize = 16;
        public int TextPadding = 40;
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
            }

            this.Parent.Left = left;

            Invalidate();
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
                        Action = HitTestAction.ItemClicked
                    };
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
            if (this.BackgroundImage != null)
            {
                var processedBackgroundImage = (Bitmap)this.BackgroundImage.Clone();
                _imageProcessor.Process(processedBackgroundImage);
                g.DrawImage(processedBackgroundImage, Point.Empty);
                g.FillRectangle(BrushCreator.CreateSolidBrush("200, 200, 200, 200"), this.ClientRectangle);            
            }

            if (!DesignMode)
            {
                foreach (var item in this.items)
                    DrawItem(item, g);
            }
            else
            {
                g.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
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
            {
                g.FillRectangle(BrushCreator.CreateSolidBrush("128, 128, 128, 128"), item.Boundary);
                g.DrawRectangle(PenCreator.Create("128, 255, 255, 255"), new Rectangle(-1, item.Boundary.Y, this.Width, item.Boundary.Height));
            }

            if (item.IsClicked)
            {               
                g.FillRectangle(BrushCreator.CreateSolidBrush("255, 0, 0, 255"), new Rectangle(0, item.Boundary.Y + 1, 3, item.Boundary.Height - 1));
            }

            if (item.Icon != null)
                g.DrawImage(item.Icon, item.IconBoundary);
           
            g.DrawString(item.Text, this.Font, BrushCreator.CreateSolidBrush("255, 0, 0, 0"), item.TextPosition);

            Rectangle leftArrowRect = new Rectangle();
            leftArrowRect.Height = item.Boundary.Height;
            leftArrowRect.Width = item.Boundary.Height / 2;
            leftArrowRect.X = item.Boundary.X + item.Boundary.Width - leftArrowRect.Width + 1;
            leftArrowRect.Y = item.Boundary.Y;                                 
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
            item.IconBoundary = new Rectangle(x + IconPadding, y + IconPadding, IconSize, IconSize);
            item.TextPosition = new Point(x + TextPadding, y + (this.ItemHeight - TextRenderer.MeasureText(item.Text, this.Font).Height) / 2);
            y += this.ItemHeight;
        }
    }    
}
