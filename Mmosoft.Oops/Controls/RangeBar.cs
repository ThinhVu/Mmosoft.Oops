using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops
{
    // TODO: Apply new style as applied for TrackBar
    [Serializable]
    public class RangeBar : Control
    {
        public static class Colors
        {
            public static string Bar = "#A";
            public static string BarDisabled = "#0";
            public static string BarHovered = "#B";

            public static string Range = "#8";
            public static string RangeDisabled = "#0";
            public static string RangeHovered = "#9";

            public static string Dot = "#8";
            public static string DotDisabled = "#0";
            public static string DotHovered = "#9";
        }

        private const int BAR_HEIGHT = 5;
        private const int LEFT_RIGHT_PADDING = 14;
        private const int HEIGHT = 13;        
        private const int DOT_RADIUS = 4;
        private const int DOT_RADIUS_ACTIVE = 6;

        private int availableWidth;
        private decimal pxPerUnit;

        private Rectangle barRect;
        private Rectangle rangeValue1Rect;
        private Rectangle rangeValue2Rect;        
        private Rectangle dot1Rect;
        private Rectangle dot2Rect;

        private bool isHovered;
        private bool isDot1Hovered;
        private bool isDot2Hovered;

        // 0: none, 1: dot1, 2: dot2
        private int selectedDot;

        private Point mouseDownLastLocation;
        private bool mouseIsDown;

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

        protected decimal value;
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

        protected decimal secondValue;
        [Browsable(true)]
        public decimal SecondValue
        {
            get { return secondValue; }
            set
            {
                if (minValue <= value && value <= maxValue)
                {
                    if (this.secondValue != value)
                    {
                        this.secondValue = value;
                        CalculateSize();
                        Invalidate();
                    }
                }
            }
        }

        public RangeBar() : base()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Range Range
        {
            get
            {
                if (value < secondValue)
                {
                    return new Range { Min = value, Max = secondValue };
                }
                else
                {
                    return new Range { Min = secondValue, Max = value };
                }
            }
            set
            {
                Value = value.Min;
                SecondValue = value.Max;
            }
        }        

        protected void CalculateSize()
        {
            rangeValue1Rect = new Rectangle(LEFT_RIGHT_PADDING, barRect.Y, (int)(pxPerUnit * value), BAR_HEIGHT);
            rangeValue2Rect = new Rectangle(LEFT_RIGHT_PADDING, barRect.Y, (int)(pxPerUnit * secondValue), BAR_HEIGHT);

            // dot 1
            int dotRadius = isDot1Hovered ? DOT_RADIUS_ACTIVE : DOT_RADIUS;
            int dotTopPadding = (HEIGHT - dotRadius * 2) / 2;
            int dotPointPosition = (int)(pxPerUnit * value);
            dot1Rect = new Rectangle(LEFT_RIGHT_PADDING + dotPointPosition - dotRadius, dotTopPadding, dotRadius * 2, dotRadius * 2);

            // dot 2
            int dotRadius2 = isDot2Hovered ? DOT_RADIUS_ACTIVE : DOT_RADIUS;
            int dotTopPadding2 = (HEIGHT - dotRadius2 * 2) / 2;
            int dotPointPosition2 = (int)(pxPerUnit * secondValue);
            dot2Rect = new Rectangle(LEFT_RIGHT_PADDING + dotPointPosition2 - dotRadius2, dotTopPadding2, dotRadius2 * 2, dotRadius2 * 2);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            bool isDot1MouseDown = dot1Rect.Contains(e.Location);
            bool isDot2MouseDown = dot2Rect.Contains(e.Location);

            if (isDot1MouseDown || isDot2MouseDown)
            {
                mouseIsDown = true;
                mouseDownLastLocation = e.Location;
            }

            selectedDot = isDot1MouseDown ? 1 : isDot2MouseDown ? 2 : 0;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseIsDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            isDot1Hovered = dot1Rect.Contains(e.Location);
            isDot2Hovered = dot2Rect.Contains(e.Location);
            if (!mouseIsDown)
            {
                CalculateSize();
                Invalidate();
            }
            else
            {
                // calculate change
                int offsetX = e.Location.X - mouseDownLastLocation.X;
                mouseDownLastLocation = e.Location;

                if (selectedDot == 1)
                    Value += (offsetX / pxPerUnit);
                else if (selectedDot == 2)
                    SecondValue += (offsetX / pxPerUnit);
                // else none
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = HEIGHT;
            availableWidth = this.Width - 1 - 2 * LEFT_RIGHT_PADDING;
            barRect = new Rectangle(LEFT_RIGHT_PADDING, (HEIGHT - BAR_HEIGHT) / 2, availableWidth, BAR_HEIGHT);
            pxPerUnit = availableWidth / maxValue;
            CalculateSize();
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            isDot1Hovered = false;
            isDot2Hovered = false;
            selectedDot = 0;
            CalculateSize();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            string barColor = null;
            string rangeColor = null;
            string dotColor = null;

            if (!Enabled)
            {
                barColor = Colors.BarDisabled;
                rangeColor = Colors.RangeDisabled;
                dotColor = Colors.DotDisabled;
            }
            else if (isHovered)
            {
                barColor = Colors.BarHovered;
                rangeColor = Colors.RangeHovered;
                dotColor = Colors.DotHovered;
            }
            else
            {
                barColor = Colors.Bar;
                rangeColor = Colors.Range;
                dotColor = Colors.Dot;
            }

            // bar background
            g.FillRectangle(BrushCreator.CreateSolidBrush(barColor), barRect);            

            // rangebar range
            if (value < secondValue)
            {
                g.FillRectangle(BrushCreator.CreateSolidBrush(rangeColor), rangeValue2Rect);
                g.FillRectangle(BrushCreator.CreateSolidBrush(barColor), rangeValue1Rect);                
            }
            else
            {
                g.FillRectangle(BrushCreator.CreateSolidBrush(rangeColor), rangeValue1Rect);
                g.FillRectangle(BrushCreator.CreateSolidBrush(barColor), rangeValue2Rect);
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;            
            g.FillEllipse(BrushCreator.CreateSolidBrush(dotColor), dot1Rect);
            g.FillEllipse(BrushCreator.CreateSolidBrush(dotColor), dot2Rect);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        }
    }

    [Serializable]
    public class Range
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}
