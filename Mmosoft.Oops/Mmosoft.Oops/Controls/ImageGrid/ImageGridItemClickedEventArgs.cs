using System;
using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public class ImageGridItemClickedEventArgs : EventArgs
    {
        public Image Image { get; set; }
        public int Index { get; set; }
    }
}
