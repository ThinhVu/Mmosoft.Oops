using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Layers
{
    [Serializable]
    public class FillBackgroundRectDrawer : BaseDrawer
    {
        public FillBackgroundRectDrawer(Control target) : base(target)
        {
        }

        public override void Draw(Graphics g, Rectangle r)
        {
            if (!targetCtrl.Visible) return;

            SolidBrush br = (
                !targetCtrl.Enabled
                    ? BrushCreator.CreateSolidBrush("#0")
                    : IsHovered
                        ? BrushCreator.CreateSolidBrush("#0")
                        : targetCtrl.Focused
                            ? BrushCreator.CreateSolidBrush("#0")
                            : BrushCreator.CreateSolidBrush("#F")
            );

            g.FillRectangle(br, r);
        }
    }
}
