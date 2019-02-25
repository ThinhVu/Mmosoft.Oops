using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Mmosoft.Oops.Controls
{
    public class Card : Control
    {
        Animation.Animator mouseEnterAnim;
        Animation.Animator mouseLeaveAnim;

        private Image image;
        [Browsable(true)]
        public Image Image
        {
            get { return image; }
            set { image = value; Invalidate(); }
        }

        private int shadow = 1;
        [Browsable(true)]
        public int Shadow
        {
            get { return shadow;  }
            set
            {
                if (value >= 0)
                {
                    shadow = value; Invalidate();
                }
            }
        }        

        public Card()
        {   
            DoubleBuffered = true;
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);


            this.Width = 100;
            this.Height = 150;        

            InitMouseEnterAnimation();
            InitMouseLeaveAnimation();            
        }

        private void InitMouseEnterAnimation()
        {
            mouseEnterAnim = new Animation.Animator()
            {
                OnStopped = () => Shadow = 0
            };
            mouseEnterAnim.Add(new Animation.Step
            {
                TotalStep = 4,
                Interval = 50,
                AnimAction = (i) => Shadow++
            });
        }
        private void InitMouseLeaveAnimation()
        {
            mouseLeaveAnim = new Animation.Animator()
            {
                OnStopped = () => Shadow = 0
            };

            mouseLeaveAnim.Add(new Animation.Step
            {
                TotalStep = 4,
                Interval = 50,
                AnimAction = (i) => Shadow--
            });
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;
            mouseLeaveAnim.Stop();
            mouseEnterAnim.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
            mouseEnterAnim.Stop();
            mouseLeaveAnim.Start();
        }
     
        protected override void OnPaint(PaintEventArgs e)
        {            
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int padding = 5;

            var cardRect = new Rectangle(padding, 0, this.Width - 2 * padding, this.Height - padding);

            if (shadow != 0)
            {
                // shadowleft
                var leftShadowPoint = new Point[] {
                    new Point(cardRect.Left, 0), // right top
                    new Point(cardRect.Left - shadow, cardRect.Bottom), // left bottom
                    new Point(cardRect.Left,  cardRect.Bottom) // right bottom
                };
                var leftShadowBrush = new LinearGradientBrush(
                    new Rectangle(cardRect.Left - shadow, 0, shadow, cardRect.Height),
                    CustomColorTranslator.Get("#0f"), CustomColorTranslator.Get("#80"), LinearGradientMode.ForwardDiagonal);
                g.FillPolygon(leftShadowBrush, leftShadowPoint);

                // shadow right
                var rightShadowPoint = new Point[] {
                    new Point(cardRect.Right, 0), // left top
                    new Point(cardRect.Right, cardRect.Bottom), // left bottom
                    new Point(cardRect.Right + shadow, cardRect.Bottom) // right bottom
                };
                var rightShadowBrush = new LinearGradientBrush(
                    new Rectangle(cardRect.Right, 0, shadow, cardRect.Bottom),
                    CustomColorTranslator.Get("#0f"), CustomColorTranslator.Get("#80"), LinearGradientMode.BackwardDiagonal);
                g.FillPolygon(rightShadowBrush, rightShadowPoint);

                // shadow bottom
                int bottom = cardRect.Bottom;
                var bottomPoint = new Point[]
                {
                    new Point(cardRect.Left - shadow, bottom - 1), // left top
                    new Point(cardRect.Right + shadow, bottom - 1), // right top
                    new Point(cardRect.Right + shadow, bottom + shadow), // right bot
                    new Point(cardRect.Left - shadow, bottom + shadow), // left bot
                };
                var bottomShaDowBrush = new LinearGradientBrush(
                    new Rectangle(cardRect.Left - shadow, bottom - 1, cardRect.Width + 2 * shadow, shadow + 10),
                    CustomColorTranslator.Get("#80"), CustomColorTranslator.Get("#0f"), LinearGradientMode.Vertical);               
                    
                g.FillPolygon(bottomShaDowBrush, bottomPoint);
            }            

            if (this.Image != null)
                g.DrawImage(this.Image, cardRect);
            else
                g.FillRectangle(new SolidBrush(Color.White), cardRect);

        }
    }
}
