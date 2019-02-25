using IP;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Layers
{
    [Serializable]
    public class BlurDrawer : BaseDrawer
    {
        private bool processing;
        public int BlurLevel { get; set; }

        public BlurDrawer(Control target) : base(target)
        {
            BlurLevel = 1;
        }

        public override void Draw(Graphics g, Rectangle r)
        {
            base.Draw(g, r);

            if (!IsDesignMode)
            {
                if (!processing)
                {
                    processing = true;

                    using (Bitmap b = new Bitmap(targetCtrl.Width, targetCtrl.Height))
                    {
                        targetCtrl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));

                        // do something with bitmap
                        var ip = new ImageProcessor();
                        for (int i = 0; i < BlurLevel; i++)
                        {
                            ip.Filters.Add(new IP.Core.Filters.BlurFilter());                            
                        }
                        ip.Process(b);

                        g.DrawImage(b, new PointF(0, 0));
                    }

                    processing = false;
                }
            }
        }
    }
}
