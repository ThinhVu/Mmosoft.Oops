using IP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Layers
{
    [Serializable]
    public class GrayScaleDrawer : BaseDrawer
    {
        private bool processing;
        
        public GrayScaleDrawer(Control target) : base(target)
        {            
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
                        ip.Filters.Add(new IP.Core.Filters.GrayScaleFilter());
                        ip.Process(b);

                        g.DrawImage(b, new PointF(0, 0));
                    }

                    processing = false;
                }
            }
        }
    }
}
