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
        private int _contentPadding;
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

        private LinearGradientBrush _topBrush;
        private LinearGradientBrush _bottomBrush;
        private LinearGradientBrush _leftBrush;
        private LinearGradientBrush _rightBrush;

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

            this.Width = 100;
            this.Height = 150;            
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
           // g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle topRect = new Rectangle(8, 0, this.Width - 16, 8);
            Rectangle rightRect = new Rectangle(this.Width - 8, 8, 8, this.Height - 16);
            Rectangle bottomRect = new Rectangle(8, this.Height - 8, this.Width - 16, 8);
            Rectangle leftRect = new Rectangle(-1, 8, 8, this.Height - 16);
            

            _topBrush = new LinearGradientBrush(topRect, Color.FromArgb(128, Color.Black), Color.White, -90f);
            _bottomBrush = new LinearGradientBrush(bottomRect, Color.FromArgb(128, Color.Black), Color.White, 90f);
            _leftBrush = new LinearGradientBrush(leftRect, Color.FromArgb(128, Color.Black), Color.White, 180f);
            _rightBrush = new LinearGradientBrush(rightRect, Color.FromArgb(128, Color.Black), Color.White, 0f);

            g.FillRectangle(_topBrush, topRect);
            g.FillRectangle(_bottomBrush, bottomRect);
            g.FillRectangle(_leftBrush, leftRect);
            g.FillRectangle(_rightBrush, rightRect);
           
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
                _topBrush.Dispose();
            }
        }
    }
}
