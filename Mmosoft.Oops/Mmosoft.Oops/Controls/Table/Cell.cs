using System.Drawing;

namespace Mmosoft.Oops.Controls.Table
{
    class Cell
    {
        public Rectangle Bounds { get; set; }
        public string Text { get; set; }

        public Cell(string text, Rectangle bounds)
        {
            this.Text = text;
            this.Bounds = bounds;
        }
    }
}