using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public class BeforeAfterImage : Control
    {
        //
        private const int SEPARATE_BUTTON_SIZE = 30;

        private int _sepearateLinePosition;
        private Rectangle _separateButton;
        private Bitmap _moveIcon;
        private bool _isMoving;
        // 
        private Image _before;
        private Image _after;

        // 
        private Pen _separatePen;

        //
        public Image Before { get { return _before; } set { _before = value; Invalidate(); } }
        public Image After { get { return _after; } set { _after = value; Invalidate(); } }

        //
        public BeforeAfterImage()
        {
            DoubleBuffered = true;

            _moveIcon = SvgPath8x8Mgr.Get("M3 1.5l-3 2.5 3 2.5v-2h2v2l3-2.5-3-2.5v2h-2v-2z", 4, Brushes.Black);
            _separatePen = new Pen(Color.FromArgb(128, Color.Black));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (_separateButton == Rectangle.Empty)
            {
                _separateButton = new Rectangle
                {
                    X = - SEPARATE_BUTTON_SIZE / 2,
                    Y = this.Height / 2 - SEPARATE_BUTTON_SIZE / 2,
                    Width = SEPARATE_BUTTON_SIZE,
                    Height = SEPARATE_BUTTON_SIZE
                };
            }
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {            
            base.OnMouseDown(e);
            _isMoving = _separateButton.Contains(e.Location);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isMoving = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_separateButton.Contains(e.Location) || _isMoving)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;

            if (_isMoving)
            {
                if (e.Location.X < 0 || e.Location.X > this.Width)
                    return;                
                _sepearateLinePosition = e.Location.X;
                _separateButton = new Rectangle
                {
                    X = _sepearateLinePosition - SEPARATE_BUTTON_SIZE / 2,
                    Y = this.Height / 2 - SEPARATE_BUTTON_SIZE / 2,
                    Width = SEPARATE_BUTTON_SIZE,
                    Height = SEPARATE_BUTTON_SIZE
                };
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (_after != null)
                g.DrawImageUnscaledAndClipped(_after, new Rectangle(0, 0, _after.Width, _after.Height));

            if (_before != null)
            {
                int w = (int)Math.Min(_sepearateLinePosition, _before.Width);
                g.DrawImageUnscaledAndClipped(_before, new Rectangle(0, 0, w, _before.Height));

                if (_sepearateLinePosition <= _before.Width)
                {
                    g.DrawLine(_separatePen, new Point(_sepearateLinePosition, 0), new Point(_sepearateLinePosition, this.Height));
                    g.FillEllipse(Brushes.White, _separateButton);
                    // g.FillRectangle(Brushes.White, _separateButton);
                    // g.DrawRectangle(Pens.Black, _separateButton);
                    g.DrawEllipse(Pens.Black, _separateButton);
                    g.DrawImage(_moveIcon, _separateButton.DecreaseSizeFromCenter(10, 10));
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _separatePen.Dispose();
            }
        }
    }
}
