using System.Drawing;

namespace Mmosoft.Oops.Controls.TitleBar
{
    class TitleBarControlButton
    {
        public bool IsMouseHover { get; set; }
        public Rectangle Boundary { get; set; }
        public Rectangle ImageBoundary { get; set; }
        public Image Image { get; set; }

        public bool Contains(Point location)
        {
            return this.Boundary.Contains(location);
        }
    }
}
