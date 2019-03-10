using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    [Serializable]
    public class NavBarItemWrapper
    {        
        /// <summary>
        /// Absolute boundary of current item from original point (0, 0)
        /// </summary>
        public Rectangle Boundary { get; set; }
        /// <summary>
        /// Absolute boundary of current item drop down icon from original point (0, 0);
        /// </summary>
        public Rectangle DropDownButtonBoundary { get; set; }
        /// <summary>
        /// Position of text content
        /// </summary>
        public Point TextPosition { get; set; }
        /// <summary>
        /// Absolute boundary of current items icon from original point (0, 0)
        /// </summary>
        public Rectangle IconBoundary { get; set; }
        /// <summary>
        /// Indicate whether current item is being Expanded or not
        /// If the current item is being expanded, an up arrow will be shown
        /// Otherwise, down arrow.
        /// 
        /// Note that the arrow only show if current item contain child item(s).
        /// </summary>
        public bool IsExpanded;
        /// <summary>
        /// Indicate whether current item is being hovered or not
        /// If the current item is being hoverred, ColorPallete will be filled
        /// </summary>
        public bool IsHovered;
        /// <summary>
        /// Indicate whether the current item is being clicked or not
        /// If the current item is being clicked, ColorPallete will be filled.
        /// </summary>
        public bool IsClicked;
        /// <summary>
        /// Content
        /// </summary>
        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        /// <summary>
        /// Clicked event
        /// </summary>
        public EventHandler Clicked;

        public NavBarItemWrapper(NavBarItem item)
        {
            this.Clicked = item.Clicked;
            this.Text = item.Text;
            this.Icon = item.Icon;        
        }

        public override string ToString()
        {
            return "";
        }
    }
}
