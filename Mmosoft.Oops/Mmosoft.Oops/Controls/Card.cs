using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Mmosoft.Oops.Controls
{
    [Designer(typeof(ParentControlDesigner))]
    public class Card : Control
    {
        Animation.Animator mouseEnterAnim;
        Animation.Animator mouseLeaveAnim;

        private int _contentPadding;    
        private int _shadowPadding;

        protected Rectangle ContentRectangle
        {
            get 
            {
                return new Rectangle(
                    _contentPadding,
                    _contentPadding,
                    this.Width - 1 - 2 * _contentPadding,
                    this.Height - 1 - 2 * _contentPadding);
            }
        }

        private SolidBrush _shadowBrush;

        private Image image;
        [Browsable(true)]
        public Image Image
        {
            get { return image; }
            set { image = value; Invalidate(); }
        }     

        public Card()
        {   
            DoubleBuffered = true;
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);

            _contentPadding = 5;
            _shadowPadding = 5;

            this.Width = 100;
            this.Height = 150;

            _shadowBrush = BrushCreator.CreateSolidBrush("#4F000000");
            InitMouseEnterAnimation();
            InitMouseLeaveAnimation();            
        }

        private void InitMouseEnterAnimation()
        {
            mouseEnterAnim = new Animation.Animator()
            {
                OnStopped = () => _shadowPadding = 0
            };
            mouseEnterAnim.Add(new Animation.Step
            {
                TotalStep = 5,
                Interval = 20,
                AnimAction = (i) => { _shadowPadding -= 1; Invalidate(); }
            });
        }
        private void InitMouseLeaveAnimation()
        {
            mouseLeaveAnim = new Animation.Animator()
            {
                OnStopped = () => _shadowPadding = 5
            };
            mouseLeaveAnim.Add(new Animation.Step
            {
                TotalStep = 5,
                Interval = 20,
                AnimAction = (i) => { _shadowPadding += 1; Invalidate(); }
            });
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {         
            mouseLeaveAnim.Stop();
            mouseEnterAnim.Start();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseEnterAnim.Stop();
            mouseLeaveAnim.Start();
        }

        protected override void OnMouseEnter(EventArgs e)
        {            
            this.Cursor = Cursors.Hand;            
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;            
        }
     
        protected override void OnPaint(PaintEventArgs e)
        {            
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var shadowRect = new RectangleF(
                _contentPadding + _shadowPadding,
                _contentPadding + _shadowPadding,
                this.ContentRectangle.Width,
                this.ContentRectangle.Height);

            g.FillRectangle(_shadowBrush, shadowRect);

            if (this.Image != null)
            {
                g.DrawImage(this.Image, ContentRectangle);
            }
            else
                g.FillRectangle(new SolidBrush(Color.White), ContentRectangle);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _shadowBrush.Dispose();
            }
        }
    }
}
