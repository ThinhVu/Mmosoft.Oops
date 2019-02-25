using System.Drawing;

namespace Mmosoft.Oops.Controls.Table
{
    public class Cell
    {
        public Rectangle CellBoundary { get; set; }
        public Rectangle TextBoundary { get; set; }

        public string Text { get; set; }
    }
}