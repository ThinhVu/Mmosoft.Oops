using System;

namespace Mmosoft.Oops
{
    [Serializable]
    public class HitTestItem
    {        
        public NavBarItemWrapper Item { get; set; }
        public HitTestAction Action { get; set; }
    }
}
