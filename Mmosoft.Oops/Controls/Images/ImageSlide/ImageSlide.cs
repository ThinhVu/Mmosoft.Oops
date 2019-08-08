using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Images
{
    public class ImageSlide : Control
    {
        private int NAV_SIZE = 50;
        private bool drawPrev;
        private Rectangle prevRect;
        private Image prevImage;

        private bool drawNext;
        private Rectangle nextRect;
        private Image nextImage;

        private List<Image> imgs;
        private Rectangle imgRect;
        private int currentIndex;

        public ImageSlide()
        {
            DoubleBuffered = true;

            imgs = new List<Image>();
            currentIndex = -1;
            prevRect = new Rectangle(20, 0, NAV_SIZE, NAV_SIZE);
            prevImage = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowCircleLeft, 10, Brushes.White);
            nextRect = new Rectangle(0, 0, NAV_SIZE, NAV_SIZE);
            nextImage = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowCircleRight, 10, Brushes.White);
        }

        public void AddImage(Image image)
        {
            imgs.Add(image);
            if (currentIndex < 0)
            {
                currentIndex = 0;
                RecalcImageRect();
                Invalidate();
            }

            RecalcNextRect();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (imgs.Count > 0)
                RecalcImageRect();

            int navTop = (this.Height - prevRect.Height) / 2;
            int navNextLeft = this.Width - nextRect.Width - 20; // padding

            prevRect = new Rectangle(20, navTop, NAV_SIZE, NAV_SIZE);
            nextRect = new Rectangle(navNextLeft, navTop, NAV_SIZE, NAV_SIZE);

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Cursor = ( prevRect.Contains(e.Location) || nextRect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int curTmpIndex = currentIndex;

            if (prevRect.Contains(e.Location))
            {
                curTmpIndex--;
                if (curTmpIndex < 0)
                    curTmpIndex = 0;
                
            }
            else if (nextRect.Contains(e.Location))
            {
                curTmpIndex++;
                if (curTmpIndex == imgs.Count)
                    curTmpIndex = imgs.Count - 1;
            }

            drawPrev = imgs.Count > 0 && curTmpIndex > 0;
            drawNext = imgs.Count > 0 && curTmpIndex < imgs.Count - 1;

            if (currentIndex != curTmpIndex)
            {
                currentIndex = curTmpIndex;
                RecalcImageRect();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (imgs == null || imgs.Count == 0) return;
            var g = e.Graphics;
            g.DrawImage(imgs[currentIndex], imgRect);
            if (drawPrev) g.DrawImage(prevImage, prevRect);
            if (drawNext) g.DrawImage(nextImage, nextRect);
        }

        private void RecalcImageRect()
        {
            imgRect = ImageDisplayModeHelper.GetImageRect(this.ClientRectangle, new Rectangle(Point.Empty, imgs[currentIndex].Size), DisplayMode.ScaleLossLessCenter);
        }

        private void RecalcNextRect()
        {
            bool nextDrawNext = imgs.Count > 0 && currentIndex < imgs.Count - 1;
            if (drawNext != nextDrawNext)
            {
                drawNext = nextDrawNext;
                Invalidate();
            }
        }
    }
}
