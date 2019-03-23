using System;

namespace Mmosoft.Oops.Controls
{
    [Serializable]
    class HitTestItem
    {        
        public NavBarItemWrapper Item { get; set; }
        public HitTestAction Action { get; set; }
    }
}
