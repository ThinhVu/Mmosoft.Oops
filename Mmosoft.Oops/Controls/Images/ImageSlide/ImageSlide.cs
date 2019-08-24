using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mmosoft.Oops.Animation;

namespace Mmosoft.Oops.Controls.Images
{
    public class ImageSlide : Control
    {
        /// <summary>
        /// Set LazyMode to true if you're lazy
        /// In this mode, instead of click to navigation button to move between images
        /// you just need click Mouse Left to go to prev image, and mouse right to move to next image
        /// </summary>
        public bool LazyMode = false;

        #region NAV
        private int NAV_SIZE = 50;

        // Prev button
        private bool drawPrev;
        private Rectangle prevNavRect;
        private Image prevNavImage;

        // Next button
        private bool drawNext;
        private Rectangle nextNavRect;
        private Image nextNavImage;
        #endregion

        //
        // Navigation
        public Action OnEscape;
        public Keys Escape = Keys.Escape;
        public Keys Prev = Keys.Left;
        public Keys Next = Keys.Right;

        // Image list
        private List<Image> imgs;

        // 
        private RectangleF imgRect;
        private int index;

        public ImageSlide()
        {
            DoubleBuffered = true;

            imgs = new List<Image>();
            index = -1;
            prevNavRect = new Rectangle(20, 0, NAV_SIZE, NAV_SIZE);
            prevNavImage = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowCircleLeft, 10, Brushes.White);

            nextNavRect = new Rectangle(0, 0, NAV_SIZE, NAV_SIZE);
            nextNavImage = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowCircleRight, 10, Brushes.White);
        }

        public void AddImage(Image image)
        {
            imgs.Add(image);
            // animation when the first image added into control
            if (index == -1)
            {
                index = 0;
                ReDraw();
            }
        }

        public void Clear()
        {
            foreach (var img in imgs)
            {
                img.Dispose();
            }

            imgs = new List<Image>();
            index = -1;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Focus();
            ReDraw();
        } 

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            
            int navTop = (this.Height - prevNavRect.Height) / 2;
            int navNextLeft = this.Width - nextNavRect.Width - 20; // padding

            prevNavRect = new Rectangle(20, navTop, NAV_SIZE, NAV_SIZE);
            nextNavRect = new Rectangle(navNextLeft, navTop, NAV_SIZE, NAV_SIZE);

            if (imgs.Count > 0)
            {
                ReDraw();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Cursor = ( prevNavRect.Contains(e.Location) || nextNavRect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (drawPrev && prevNavRect.Contains(e.Location) || (LazyMode && e.Button == System.Windows.Forms.MouseButtons.Left))
            {
                if (index > 0)
                {
                    index--;
                    ReDraw();
                }
            }
            else if (drawNext && nextNavRect.Contains(e.Location) || (LazyMode && e.Button == System.Windows.Forms.MouseButtons.Right))
            {
                if (index < imgs.Count - 1)
                {
                    index++;
                    ReDraw();
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == this.Prev)
            {
                if (index > 0)
                {
                    index--;
                    ReDraw();
                }
            }
            else if (e.KeyCode == this.Next)
            {
                if (index < imgs.Count - 1)
                {
                    index++;
                    ReDraw();
                }
            }
            else if (e.KeyCode == this.Escape)
            {
                if (OnEscape != null)
                    OnEscape();
            }
        }

        private void RecalcImageRect()
        {
            if (-1 < index && index < imgs.Count)
            {
                imgRect = ImageDisplayModeHelper.GetImageRect(
                    this.ClientRectangle,
                    new Rectangle(Point.Empty, imgs[index].Size),
                    DisplayMode.ScaleLossLessCenter);
            }
        }

        private void ReDraw()
        {
            RecalcImageRect();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (imgs == null || imgs.Count == 0) 
                return;

            var g = e.Graphics;
            if (index > -1 && index < imgs.Count)
            {
                
                g.DrawImage(imgs[index], imgRect);
            }

            if (!LazyMode && drawPrev)
                g.DrawImage(prevNavImage, prevNavRect);

            if (!LazyMode && drawNext)
                g.DrawImage(nextNavImage, nextNavRect);
        }
    }
}
