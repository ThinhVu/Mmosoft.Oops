using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops
{
    [Serializable]
    public partial class ProgressBar : Control
    {
        private Pen pen;

        private PointF startPoint;
        private PointF endPoint;
        private PointF valuePoint;

        private float progressWidth;
        private float padding;
        private bool mouseOver;
        
        private decimal minValue;
        [Browsable(true)]
        public decimal MinValue
        {
            get { return minValue; }
            set
            {
                var v = value < 0 ? 0 : value;
                if (minValue != v)
                {
                    minValue = v;
                    UpdateProgressUI();
                }
            }
        }

        private decimal maxValue;
        [Browsable(true)]
        public decimal MaxValue
        {
            get { return maxValue; }
            set
            {
                var v = value < 0 ? 0 : value;
                if (maxValue != v)
                {
                    maxValue = v;
                    UpdateProgressUI();
                }
            }
        }

        private decimal value;
        [Browsable(true)]
        public decimal Value
        {
            get { return value; }
            set
            {
                if (minValue <= value && value <= maxValue)
                {
                    if (this.value != value)
                    {
                        this.value = value;
                        UpdateProgressUI();
                    }
                }
            }
        }

        public ProgressBar()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            pen = PenCreator.Create("#0");
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            MinValue = 0;
            MaxValue = 100;
            Height = 5;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseOver = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseOver = false;
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // change pen width
            pen.Width = Height;
            
            // calculate progress
            int avH = Height - 1;
            int avW = Width - 1;
            padding = avH / 2;
            progressWidth = avW - 2 * padding;
            startPoint = new PointF(padding, padding);
            endPoint = new PointF(avW - padding, padding);
            UpdateProgressUI();
        }

        private void UpdateProgressUI()
        {
            valuePoint = new PointF((float)((decimal)progressWidth * value / maxValue), padding);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            string barBgColor = null;
            string progressColor = null;

            if (!Enabled)
            {
                barBgColor = ProgressBarColors.BarBackgoundDisabled;
                progressColor = ProgressBarColors.ProgressBackgoundDisabled;
            }
            else if (mouseOver)
            {
                barBgColor = ProgressBarColors.BarBackgoundHovered;
                progressColor = ProgressBarColors.ProgressBackgoundHovered;
            }
            else
            {
                barBgColor = ProgressBarColors.BarBackgound;
                progressColor = ProgressBarColors.ProgressBackgound;
            }
            
            // draw progress color
            pen.Color = CustomColorTranslator.Get(barBgColor);
            g.DrawLine(pen, startPoint, endPoint);
            // draw progress value
            if (value > 0)
            {
                pen.Color = CustomColorTranslator.Get(progressColor);
                g.DrawLine(pen, startPoint, valuePoint);
            }                
        }
    }
}
