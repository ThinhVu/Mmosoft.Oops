using System;

namespace Mmosoft.Oops.SingleLevelNavBar
{
    [Serializable]
    public class HitTestItem
    {        
        public NavBarItemWrapper Item { get; set; }
        public HitTestAction Action { get; set; }
    }
}
