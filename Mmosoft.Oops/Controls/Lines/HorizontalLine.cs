using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Lines
{
    public class HorizontalLine : Control
    {
        private Pen _linePen;
        private Point start;
        private Point end;

        private int lineWeight;
        [Browsable(true)]
        public int LineWeight
        {
            get { return lineWeight; }
            set { lineWeight = value; _linePen.Width = lineWeight; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Define line color for control")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LineColors LineColors
        {
            get { return _lineColors; }
            set { _lineColors = value; Invalidate(); }
        }
        private LineColors _lineColors;

        public HorizontalLine()
        {
            _lineColors = new LineColors();
            _linePen = PenCreator.Create(_lineColors.LineColor, 1f);

            DoubleBuffered = true;
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);

            start = new Point(0, 1);
            LineWeight = 1;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            end = new Point(this.Width, 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            
            e.Graphics.DrawLine(_linePen, start, end);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _linePen.Dispose();
            }
        }
    }
}
