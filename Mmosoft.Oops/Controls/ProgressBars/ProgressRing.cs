using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public class ProgressRing : Control
    {
        private Pen _progress;
        public const int LineWidth = 5;
        private float _startAngle;
        private float _sweepAngle;
        private Timer _timer;

        [Browsable(true)]
        [Description("Set the color of progress ring")]
        public Color RingColor 
        {
            set
            {
                _progress.Color = value;
            }
        }

        public ProgressRing()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _progress = new Pen(Color.Black);
            _progress.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
            //_progress.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            //_progress.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            _progress.Width = LineWidth;

            _timer = new Timer() { Interval = 1000 / 25 };
            _timer.Tick += (s, e) =>
            {
                _startAngle+= 1;
                _sweepAngle+= 5;

                if (_startAngle >= 360)
                    _startAngle = 0;

                if (_sweepAngle >= 360)
                {
                    _sweepAngle = -355;
                }

                Invalidate();
            };

            _timer.Start();

            //
            DoubleBuffered = true;
        }      

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var ringRect = new Rectangle() { X = 2, Y = 2, Width = this.Width - 5, Height = this.Height - 5 };


            //g.DrawArc(_progress, ringRect.MoveXY(5, 5).ChangeSizeRelative(-10, -10), _startAngle - 60, _sweepAngle);
            g.DrawArc(_progress, ringRect.DecreaseSizeFromCenter(20, 20), _startAngle - 120, _sweepAngle);
            //g.DrawArc(_progress, ringRect.MoveXY(15, 15).ChangeSizeRelative(-30, -30), _startAngle - 180, _sweepAngle);
            g.DrawArc(_progress, ringRect.DecreaseSizeFromCenter(40, 40), _startAngle - 240, _sweepAngle);
            //g.DrawArc(_progress, ringRect.MoveXY(25, 25).ChangeSizeRelative(-50, -50), _startAngle - 300, _sweepAngle);

            // main
            g.DrawArc(_progress, ringRect, _startAngle, _sweepAngle);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _progress.Dispose();
            }
        }
    }
}
