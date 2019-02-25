using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Mmosoft.Oops.Colors;

namespace Mmosoft.Oops
{
    [Serializable]
    public partial class ToogleButton : Control
    {
        private const int PADDING_LEFT_RIGHT = 3;
        private const int PADDING_TOP_BOTTOM = 3;

        //
        private SolidBrush _backgroundBrush;        
        private SolidBrush _dotBrush;
        private Pen _borderPen;
        
        // 
        private Rectangle offRect;
        private Rectangle onRect;
        private PointF txtStatePosition;

        //
        private bool isHovered;

        //
        private bool isChecked;
        [Browsable(true)]
        public bool Checked
        {
            get
            {
                return isChecked;
            }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    ComputeDotPosition();
                    Invalidate();
                }
                // 
            }
        }

        private ToogleButtonColors _colors;
        public ToogleButtonColors Colors
        {
            get { return _colors; }
            set { _colors = value; Invalidate(); }
        }

        public ToogleButton()
        {
            _colors = new ToogleButtonColors();
            _backgroundBrush = BrushCreator.CreateSolidBrush(_colors.Bg);
            _dotBrush = BrushCreator.CreateSolidBrush(_colors.Dot);
            _borderPen = PenCreator.Create(_colors.Border);


            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            ComputeDotPosition();
        }
      
        protected override void OnClick(EventArgs e)
        {            
            Checked = !Checked;
            base.OnClick(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            this.Cursor = Cursors.Hand;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            this.Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ComputeDotPosition();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            string bgColor = null;
            string dotColor = null;
            string borderColor = null;

            int maxHeight = this.Height - 1;
            int maxWidth = this.Width - 1;

            if (!Enabled)
            {
                bgColor = _colors.BgDisabled;
                borderColor = _colors.BorderDisabled;
                dotColor = _colors.DotDisabled;
            }
            else if (isHovered)
            {
                bgColor = _colors.BgHovered;
                borderColor = _colors.BorderHovered;
                dotColor = _colors.DotHovered;
            }
            else if (Checked)
            {
                bgColor = _colors.BgChecked;
                borderColor = _colors.BorderChecked;
                dotColor = _colors.DotChecked;
            }
            else
            {
                bgColor = _colors.Bg;
                borderColor = _colors.Border;
                dotColor = _colors.Dot;
            }

            // bg            
            var bgBrush = BrushCreator.CreateSolidBrush(bgColor);
            g.FillEllipse(bgBrush, new Rectangle(0, 0, maxHeight, maxHeight));
            g.FillRectangle(bgBrush, new RectangleF(maxHeight / 2, 0, maxWidth - maxHeight, maxHeight));
            g.FillEllipse(bgBrush, new Rectangle(maxWidth - maxHeight, 0, maxHeight, maxHeight));

            // border
            _borderPen.Color = CustomColorTranslator.Get(borderColor);
            g.DrawArc(_borderPen, new Rectangle(0, 0, maxHeight, maxHeight), 90, 180);
            g.DrawLine(_borderPen, maxHeight / 2, 0, maxWidth - maxHeight / 2, 0);
            g.DrawLine(_borderPen, maxHeight / 2, maxHeight, maxWidth - maxHeight / 2, maxHeight);
            g.DrawArc(_borderPen, new Rectangle(maxWidth - maxHeight, 0, maxHeight, maxHeight), 270, 180);

            // dot
            g.FillEllipse(_dotBrush, isChecked ? onRect : offRect);

            // txt            
            g.DrawString(Checked ? "On" : "Off", this.Font, _dotBrush, txtStatePosition);
        }

        private void ComputeDotPosition()
        {
            int maxHeight = this.Height - 1;
            int maxWidth = this.Width - 1;            
            int dotSize = maxHeight - 2 * PADDING_TOP_BOTTOM;
            offRect = new Rectangle(PADDING_LEFT_RIGHT, PADDING_TOP_BOTTOM, dotSize, dotSize);
            onRect = new Rectangle(maxWidth - PADDING_LEFT_RIGHT - dotSize, PADDING_TOP_BOTTOM, dotSize, dotSize);
            
            if (Checked)
            {
                Size txtSize = TextRenderer.MeasureText("On", this.Font);
                txtStatePosition = new PointF(
                    PADDING_LEFT_RIGHT, 
                    PADDING_TOP_BOTTOM + (dotSize - txtSize.Height)/2);
            }            
            else 
            {
                Size txtSize = TextRenderer.MeasureText("Off", this.Font);
                txtStatePosition = new PointF(
                    offRect.Right + PADDING_LEFT_RIGHT, 
                    PADDING_TOP_BOTTOM + (dotSize - txtSize.Height) / 2);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _backgroundBrush.Dispose();
                _dotBrush.Dispose();
                _borderPen.Dispose();
            }
        }
    }
}
