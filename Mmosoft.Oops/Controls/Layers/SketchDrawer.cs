using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Layers
{
    [Serializable]
    public class SketchDrawer : BaseDrawer
    {
        public int StepX { get; set; }
        public int StepY { get; set; }
        public bool Mirror { get; set; }
        public string Color { get; set; }

        public SketchDrawer(Control target) : base(target)
        {
            StepX = 5;
            StepY = 5;
            Color = "40, 40, 40";
        }

        public override void Draw(Graphics g, Rectangle r)
        {
            int step = GetStepNeeded(r.Width, r.Height, StepX, StepY);

            if (step == 0) return;

            int x = 0;
            int y = 0;
            
            for (int i = 0; i <= step; i++)
            {
                if (StepX != 0 && StepY != 0)
                {
                    g.DrawLine(
                        PenCreator.Create(Color, 1f), // Pen
                        0, y /*Point 1*/,
                        x, 0 /*Point 2*/);
                    if (Mirror)
                    {
                        g.DrawLine(
                        PenCreator.Create(Color, 1f), // Pen
                        r.Width, y /*Point 1*/,
                        r.Width - x, 0 /*Point 2*/);
                    }
                }
                else if (StepX == 0)
                {
                    // ignore x, draw horizontal line
                    g.DrawLine(
                        PenCreator.Create(Color, 1f), // Pen
                        0, y /*Point 1*/,
                        r.Width, y /*Point 2*/);
                }
                else
                {
                    // ignore y, draw vertical line
                    g.DrawLine(
                        PenCreator.Create("40, 40, 40", 1f), // Pen
                        x, 0 /*Point 1*/,
                        x, r.Height /*Point 2*/);
                }

                x += StepX;
                y += StepY;
            }
        }


        /// <summary>
        /// Get total line needed to sketch to fill entire the target rectangle  
        /// It's really fun when touching Math XD.
        /// </summary>
        /// <param name="rWidth"></param>
        /// <param name="rHeight"></param>
        /// <param name="increaseWidth">Distance of x[i] and x[i+1]</param>
        /// <param name="increaseHeight">Distance of y[i] and y[i+1]</param>
        /// <returns></returns>
        private int GetStepNeeded(int rWidth, int rHeight, int increaseWidth, int increaseHeight)
        {
            if (StepX == 0 && StepY == 0)
            {
                return 0;
            }
            if (StepX != 0 && StepY != 0)
            {
                // see: ./Sketch.jpg
                double rHypotenuse = Math.Sqrt(Math.Pow(rWidth, 2) + Math.Pow(rHeight, 2));                
                double a1 = Math.Acos(rHeight * 1d / rHypotenuse) * 180 / Math.PI;
                double b = Math.Atan2(increaseWidth, increaseHeight) * 180 / Math.PI;                
                double a2 = 90 - b;
                double a3 = a1 - a2;
                double h1 = Math.Cos(a3 * Math.PI / 180) * rHypotenuse;
                double y = h1 / Math.Cos(a2 * Math.PI / 180);
                int n = (int)Math.Round(y / increaseHeight);
                return n;
            }
            else if (StepX == 0)
            {
                // only increase y => draw horizontal line
                int n = rHeight / increaseHeight;
                return n;
            }
            else
            {
                // only increase x => draw vertical line
                int n = rWidth / increaseWidth;
                return n;
            }
        }
    }
}
