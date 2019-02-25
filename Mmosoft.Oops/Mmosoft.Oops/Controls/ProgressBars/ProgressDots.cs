using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops
{
    [Serializable]
    public class ProgressDots : Control
    {        
        private const int DOT_NUMBER = 3;
        private const int DOT_RADIUS = 5;
        private List<Rectangle> dotRects;

        // 
        private Timer animTimer;
        
        public ProgressDots()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // 
            InitDotRect();

            // 
            animTimer = new Timer();
            animTimer.Interval = 20;
            animTimer.Tick += Timer_Tick;
            animTimer.Start();
        }

        private void InitDotRect()
        {
            dotRects = new List<Rectangle>(DOT_NUMBER);
            for (int i = 0; i < DOT_NUMBER; i++)
                dotRects.Add(new Rectangle((int)(-DOT_RADIUS * i * 1.5), DOT_RADIUS / 2, DOT_RADIUS, DOT_RADIUS));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var half = this.Width / 2;
            for (int i = 0; i < dotRects.Count; i++)
            {
                var r = dotRects[i];
                r.X += (Math.Abs(half - r.X) / 20) + DOT_RADIUS;                       
                dotRects[i] = r;
            };

            if (dotRects[DOT_NUMBER-1].X > this.Width)
            {
                InitDotRect();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            if (!DesignMode)
            {
                
                g.Clear(this.BackColor);

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                for (int i = 0; i < dotRects.Count; i++)
                {                    
                    if (i != dotRects.Count - 1)
                    {
                        g.FillEllipse(BrushCreator.CreateSolidBrush("#0"), dotRects[i]);                        
                    }
                    else
                    {
                        g.FillEllipse(BrushCreator.CreateSolidBrush("#0"), dotRects[i]);
                    }                    
                }
            }
            else
            {
                g.FillRectangle(BrushCreator.CreateSolidBrush("#0"), new Rectangle(0,0, this.Width -1, this.Height - 1));
            }
        }
    }
}
