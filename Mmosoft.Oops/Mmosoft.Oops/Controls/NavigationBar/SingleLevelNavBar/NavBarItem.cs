using System;
using System.Drawing;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    [Serializable]
    public class NavBarItem
    {
        public string Text { get; set; }
        public Bitmap Icon { get; set; }
        public EventHandler Clicked;
    }
}
