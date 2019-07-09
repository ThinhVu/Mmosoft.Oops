using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Lines
{
    public class VerticalLine : Control
    {        
        private Point _start;
        private Point _end;
        
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Define line color for control")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LineColors Colors
        {
            get { return _colors; }
            set { _colors = value; Invalidate(); }
        }
        private LineColors _colors;

        private Pen _linePen;

        private int lineWeight;
        [Browsable(true)]
        public int LineWeight
        {
            get { return lineWeight; }
            set { lineWeight = value; Invalidate(); }
        }

        public Point Start { get { return _start; } set { _start = value; } }

        public VerticalLine()
        {
            _colors = new LineColors();
            _linePen = PenCreator.Create(_colors.LineColor, 1);
            
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Start = new Point(1, 0);
            LineWeight = 1;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _end = new Point(1, this.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            _linePen.Width = lineWeight;
            g.DrawLine(_linePen, Start, _end);
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
