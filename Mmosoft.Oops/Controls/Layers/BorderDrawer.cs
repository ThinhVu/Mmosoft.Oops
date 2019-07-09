using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Layers
{
    [Serializable]
    public class BorderDrawer : BaseDrawer
    {
        public BorderDrawer(Control target) : base(target)
        {
        }

        public override void Draw(Graphics g, Rectangle r)
        {
            g.DrawRectangle(PenCreator.Create("#0"), r);
        }
    }
}
