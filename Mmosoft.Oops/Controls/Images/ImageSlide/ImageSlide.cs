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

        // flyout
        private Animator flyinAnim;
        private Animator flyoutAnim;
        private int s;
        private int vCurrentHor;
        private float vNewHor;
        private float vNewVer;
        private int step = 20; // 20 step
        private int direction = -1;

        private SolidBrush opacityBrush;
        private int opacityStep = 255 / 20; // step

        private int NAV_SIZE = 50;

        // Prev button
        private bool drawPrev;
        private Rectangle prevNavRect;
        private Image prevNavImage;

        // Next button
        private bool drawNext;
        private Rectangle nextNavRect;
        private Image nextNavImage;

        // Image list
        private List<Image> imgs;

        // 
        private RectangleF currentImgRect;
        private int currentIndex;

        // 
        private RectangleF newImgRect;
        private int newIndex;

        public ImageSlide()
        {
            DoubleBuffered = true;

            imgs = new List<Image>();

            currentIndex = -1;
            prevNavRect = new Rectangle(20, 0, NAV_SIZE, NAV_SIZE);
            prevNavImage = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowCircleLeft, 10, Brushes.White);

            nextNavRect = new Rectangle(0, 0, NAV_SIZE, NAV_SIZE);
            nextNavImage = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowCircleRight, 10, Brushes.White);

            opacityBrush = new SolidBrush(Color.Black);

            SetupFlyinAnim();
            SetupFlyoutAnim();
        }

        
        private void SetupFlyinAnim()
        {
            flyinAnim = new Animator()
            {
                OnCompleted = () =>
                {
                    currentIndex = newIndex;
                    drawPrev = imgs.Count > 0 && currentIndex > 0;
                    drawNext = currentIndex < imgs.Count - 1;
                    opacityBrush.Color = Color.FromArgb(0, Color.Black);
                    RecalcImageRect();
                }
            };
            flyinAnim.Add(new Step
            {
                Interval = 20, // ms
                TotalStep = step,
                AnimAction = (stepI) =>
                {
                    // current
                    currentImgRect = currentImgRect.AdjustXF(direction * vCurrentHor);
                    opacityBrush.Color = Color.FromArgb(255 - stepI * opacityStep, Color.Black);
                    // next
                    newImgRect = newImgRect.AdjustSizeFromCenter(vNewHor, vNewVer);
                    Invalidate();
                }
            });
        }

        private void SetupFlyoutAnim()
        {
            flyoutAnim = new Animator()
            {
                OnCompleted = () =>
                {
                    currentIndex = newIndex;
                    drawPrev = imgs.Count > 0 && currentIndex > 0;
                    drawNext = currentIndex < imgs.Count - 1;
                    opacityBrush.Color = Color.FromArgb(0, Color.Black);
                    RecalcImageRect();
                }
            };
            flyoutAnim.Add(new Step
            {
                Interval = 20, // ms
                TotalStep = step,
                AnimAction = (stepI) =>
                {
                    // current
                    currentImgRect = currentImgRect.AdjustSizeFromCenter(vNewHor, vNewVer);
                    opacityBrush.Color = Color.FromArgb(stepI * opacityStep, Color.Black);
                    // next
                    newImgRect = newImgRect.AdjustXF(direction * vCurrentHor);
                    Invalidate();
                }
            });
        }


        public void AddImage(Image image)
        {
            imgs.Add(image);
            if (currentIndex == -1)
            {
                // animation with next index
                newIndex = 0;
                FlyIn();
            }
        }

        public void Clear()
        {
            flyinAnim.Stop();
            flyoutAnim.Stop();

            foreach (var img in imgs)
            {
                img.Dispose();
            }

            imgs = new List<Image>();
            currentIndex = -1;
            newIndex = -1;
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (imgs.Count > 0)
                RecalcImageRect();

            int navTop = (this.Height - prevNavRect.Height) / 2;
            int navNextLeft = this.Width - nextNavRect.Width - 20; // padding

            prevNavRect = new Rectangle(20, navTop, NAV_SIZE, NAV_SIZE);
            nextNavRect = new Rectangle(navNextLeft, navTop, NAV_SIZE, NAV_SIZE);

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Cursor = ( prevNavRect.Contains(e.Location) || nextNavRect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            newIndex = currentIndex;
            if (drawPrev && prevNavRect.Contains(e.Location) || (LazyMode && e.Button == System.Windows.Forms.MouseButtons.Left))
            {
                newIndex--;
                if (newIndex < 0)
                    newIndex = 0;
                if (currentIndex != newIndex)
                    FlyOut();
            }
            else if (drawNext && nextNavRect.Contains(e.Location) || (LazyMode && e.Button == System.Windows.Forms.MouseButtons.Right))
            {
                newIndex++;
                if (newIndex == imgs.Count)
                    newIndex = imgs.Count - 1;
                if (currentIndex != newIndex)
                    FlyIn();
            }
        }

        private void RecalcImageRect()
        {
            currentImgRect = ImageDisplayModeHelper.GetImageRect(
                this.ClientRectangle, 
                new Rectangle(Point.Empty, imgs[currentIndex].Size), 
                DisplayMode.ScaleLossLessCenter);
            newImgRect = Rectangle.Empty;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (imgs == null || imgs.Count == 0) return;
            var g = e.Graphics;

            // show right image
            if (direction == -1)
            {
                // draw new image
                if (newIndex > -1 && newIndex < imgs.Count)
                {
                    g.DrawImage(imgs[newIndex], newImgRect);
                    g.FillRectangle(opacityBrush, newImgRect);
                }

                // draw current image
                if (currentIndex > -1 && currentIndex < imgs.Count)
                    g.DrawImage(imgs[currentIndex], currentImgRect);
            }
            // show left image
            else
            {
                // draw current image
                if (currentIndex > -1 && currentIndex < imgs.Count)
                {
                    g.DrawImage(imgs[currentIndex], currentImgRect);
                    g.FillRectangle(opacityBrush, currentImgRect);
                }

                // draw new image
                if (newIndex > -1 && newIndex < imgs.Count)
                {
                    g.DrawImage(imgs[newIndex], newImgRect);
                }
            }

            if (!LazyMode && drawPrev) g.DrawImage(prevNavImage, prevNavRect);
            if (!LazyMode && drawNext) g.DrawImage(nextNavImage, nextNavRect);
        }

        private void FlyIn()
        {
            if (currentIndex > -1)
            {
                currentImgRect = ImageDisplayModeHelper.GetImageRect(
                this.ClientRectangle,
                new Rectangle(Point.Empty, imgs[currentIndex].Size),
                DisplayMode.ScaleLossLessCenter);
                direction = -1;
                s = (int)(0.5 * (this.Width + currentImgRect.Width));
                vCurrentHor = s / step + ((s % step == 0) ? 0 : 1);
            }
            
            //
            newImgRect = ImageDisplayModeHelper.GetImageRect(
                        this.ClientRectangle,
                        new Rectangle(Point.Empty, imgs[newIndex].Size),
                        DisplayMode.ScaleLossLessCenter);
            
            int offsetSizeHor = 40;
            int offsetSizeVer = (int)(offsetSizeHor * 1f * newImgRect.Height / newImgRect.Width); 
            vNewHor = 1f * offsetSizeHor / step;
            vNewVer = 1f * offsetSizeVer / step;
            newImgRect = newImgRect.AdjustSizeFromCenter(-offsetSizeHor, -offsetSizeVer);
            flyinAnim.Start();  
        }

        private void FlyOut()
        {
            // setup to reduce size of new image
            currentImgRect = ImageDisplayModeHelper.GetImageRect(
                        this.ClientRectangle,
                        new Rectangle(Point.Empty, imgs[currentIndex].Size),
                        DisplayMode.ScaleLossLessCenter);
            int offsetSizeHor = 40;
            int offsetSizeVer = (int)(offsetSizeHor * 1f * currentImgRect.Height / currentImgRect.Width);
            vNewHor = -1f * offsetSizeHor / step;
            vNewVer = -1f * offsetSizeVer / step;

            //
            newImgRect = ImageDisplayModeHelper.GetImageRect(
                        this.ClientRectangle,
                        new Rectangle(Point.Empty, imgs[newIndex].Size),
                        DisplayMode.ScaleLossLessCenter);
            // setup prev image on the left
            newImgRect = newImgRect.AdjustXF(-newImgRect.Right);
            // and move direction is to the right
            direction = 1;
            s = (int)(0.5 * (this.Width + newImgRect.Width));
            vCurrentHor = s / step;

            flyoutAnim.Start();
        }
    }
}
