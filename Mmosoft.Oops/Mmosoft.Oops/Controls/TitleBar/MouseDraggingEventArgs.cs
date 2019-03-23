using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops.Controls.TitleBar
{
    public class MouseDraggingEventArgs : EventArgs
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
    }

    public delegate void MouseDraggingEventHandler(object sender, MouseDraggingEventArgs e);
}
