using System;
using System.Collections.Generic;

namespace Mmosoft.Oops
{
    // User friendly
    [Serializable]
    public class NavBarItem
    {        
        public List<NavBarItem> Items { get; set; }
        public string Text { get; set; }
        public EventHandler Clicked;        
    }
}
