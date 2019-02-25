using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    // Get the idea from: https://speckyboy.com/page-transition-effects/
    // Section: Speedy Transition with Preloader

    // To do the same with Loader Transition in this site, we need implement 6 timer
    // so I modify our marque progress bar a bit and using only 1 timer.
    public class MarqueeProgressBar : System.Windows.Forms.Control
    {
        private List<Bar> _bars;
        private int barPadding = 2; // padding of each bar

        // HACK: Change barnums and animStep value to get another parttern
        private int barNums = 10;
        private int activeBar = 0;

        private Timer anim;
        
        // detect if angle is going to 0 or PI.
        private double halfPi = Math.PI / 2;
        private double pi = Math.PI;
        private double oneAndHalfPi = Math.PI * 3 / 2;
        private double doublePi = Math.PI * 2;

        public MarqueeProgressBar()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            anim = new Timer() { Interval = 500, Enabled = false };
            anim.Tick += Anim_Tick;

            InitBars();
        }

        private void Anim_Tick(object sender, EventArgs e)
        {
            var avH = this.Height - 1;

            // increase active bar
            activeBar++;
            if (activeBar > _bars.Count)
                activeBar = 0;

            for (int i = 0; i < _bars.Count; i++)
            {
                var bar = _bars[i];
                
                // update zone
                var zone = bar.Zone;
                zone.Height = i == activeBar ? avH : 1;                
                // zone.Y = (avH - zone.Height);

                // store the update
                bar.Zone = zone;
            }

            Invalidate();
        }

        private bool BarHeightMoveToZero(double angle)
        {
            angle = angle % pi;

            return (halfPi < angle && angle < pi) || (oneAndHalfPi < angle && angle < doublePi);
        }

        private void InitBars()
        {            
            var avH = this.Height - 1;
            var avW = this.Width - 1;
            
            // Init bars
            float barWidth = (avW - (barNums - 1f) * barPadding) / barNums;
            int colorIncreasePerStep = byte.MaxValue / barNums;
            int barCurrentColorValue;
            List<Bar> bars = new List<Bar>();
            Bar bar;
            for(int i = 0; i < barNums; i++)
            {
                barCurrentColorValue = colorIncreasePerStep * i;
                bar = new Bar(
                    BrushCreator.CreateSolidBrush(Color.FromArgb(255, barCurrentColorValue, barCurrentColorValue, barCurrentColorValue)),
                    new RectangleF((barPadding + barWidth) * i, 0, barWidth, 0));
                bars.Add(bar);
            }

            this._bars = bars;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            InitBars();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            if (!DesignMode)
            {                
                for (int i = 0; i < _bars.Count; i++)
                    g.FillRectangle(_bars[i].Brush, _bars[i].Zone);                
            }
            else
            {
                g.DrawRectangle(PenCreator.Create("#0"), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            }
        }

        public void Start()
        {
            anim.Start();
        }

        private class Bar
        {
            public SolidBrush Brush { get; set; }
            public RectangleF Zone { get; set; }           
            public Bar(SolidBrush b, RectangleF z)
            {
                Brush = b;
                Zone = z;
            }
        }
    }
}
