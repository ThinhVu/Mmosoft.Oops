using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    // User friendly
    [Serializable]
    public class NavBarItem
    {        
        public List<NavBarItem> Items { get; set; }
        public string Text { get; set; }
        public Bitmap Icon { get; set; }
        public EventHandler Clicked;
    }
}
