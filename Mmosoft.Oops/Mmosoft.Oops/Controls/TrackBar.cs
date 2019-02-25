using Mmosoft.Oops.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops
{
    [Serializable]
    public class TrackBar : Control
    {
        public static class Colors
        {
            public static string Bar = "#A";
            public static string BarDisabled = "#0";
            public static string BarHovered = "#B";

            public static string Track = "#8";
            public static string TrackDisabled = "#0";
            public static string TrackHovered = "#9";

            public static string Dot = "#8";
            public static string DotDisabled = "#0";
            public static string DotHovered = "#9";
        }

        private const int CONTROL_HEIGHT = 13;
        private const int BAR_HEIGHT = 5;
        private const int DOT_RADIUS = 4;
        private const int DOT_RADIUS_ACTIVE = 6;

        private PointF trackPoint;
        private RectangleF viewport;
        private RectangleF dotRect;

        private Point lastMouseDownLocation;
        private bool mouseIsDownInTrackThumb;

        private bool barHovered;
        private bool dotHovered;

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
                    CalculateSize();
                    Invalidate();
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
                    CalculateSize();
                    Invalidate();
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
                        CalculateSize();
                        Invalidate();
                    }
                }
            }
        }

        Pen trackBgPen;
        Pen trackValuePen;
        SolidBrush dotBrush;

        public TrackBar() : base()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // 
            trackBgPen = PenCreator.Create(Color.Black, BAR_HEIGHT);
            trackBgPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            trackBgPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            trackValuePen = PenCreator.Create(Color.Black, BAR_HEIGHT);
            trackValuePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            trackValuePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            dotBrush = BrushCreator.CreateSolidBrush();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (dotRect.Contains(e.Location))
            {
                mouseIsDownInTrackThumb = true;
                lastMouseDownLocation = e.Location;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseIsDownInTrackThumb = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.barHovered = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!mouseIsDownInTrackThumb)
            {
                dotHovered = dotRect.Contains(e.Location);
                CalculateSize();
                Invalidate();
            }
            else
            {
                // ignore mouse move when the mouse go out of boundary
                if (e.Location.X < viewport.Left || e.Location.X > viewport.Right)
                    return;

                // otherwise, calculate change
                int xPixelChanged = e.Location.X - lastMouseDownLocation.X;
                decimal pixelsPerValueUnit = (decimal)viewport.Width / maxValue;
                decimal additionValue = xPixelChanged / pixelsPerValueUnit;
                // store last mouse position
                lastMouseDownLocation = e.Location;
                // update value and GUI
                Value += additionValue;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.barHovered = false;
            this.dotHovered = false;
            CalculateSize();
            Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            viewport = new RectangleF(DOT_RADIUS_ACTIVE, 0, (Width - 1) - 2 * DOT_RADIUS_ACTIVE, (Height - 1));
            CalculateSize();
        }

        protected void CalculateSize()
        {
            // current position of trackpoint
            trackPoint = new PointF(viewport.Left + (float)(value / maxValue) * viewport.Width, viewport.Height / 2);

            int dotRadius = dotHovered ? DOT_RADIUS_ACTIVE : DOT_RADIUS;
            int dotSize = dotRadius * 2;
            int dotVerticalPadding = ((Height - 1) - 2 * dotRadius) / 2;
            dotRect = new RectangleF(
                trackPoint.X - dotRadius,
                dotVerticalPadding,
                dotSize,
                dotSize);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            string barColor = null;
            string trackValueColor = null;
            string dotColor = null;

            if (!Enabled)
            {
                barColor = Colors.BarDisabled;
                trackValueColor = Colors.TrackDisabled;
                dotColor = Colors.DotDisabled;
            }
            else if (barHovered)
            {
                barColor = Colors.BarHovered;
                trackValueColor = Colors.TrackHovered;
                dotColor = Colors.DotHovered;
            }
            else
            {
                barColor = Colors.Bar;
                trackValueColor = Colors.Track;
                dotColor = Colors.Dot;
            }

            trackBgPen.Color = CustomColorTranslator.Get(barColor);
            trackValuePen.Color = CustomColorTranslator.Get(trackValueColor);
            dotBrush.Color = CustomColorTranslator.Get(dotColor);

            // track bar
            g.DrawLine(
                trackBgPen,
                new PointF(viewport.Left, viewport.Y + viewport.Height / 2),
                new PointF(viewport.Right, viewport.Y + viewport.Height / 2));

            // value bar
            g.DrawLine(
                trackValuePen,
                new PointF(viewport.Left, viewport.Y + viewport.Height / 2),
                trackPoint);

            // dot 1
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(dotBrush, dotRect);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        }
    }
}
