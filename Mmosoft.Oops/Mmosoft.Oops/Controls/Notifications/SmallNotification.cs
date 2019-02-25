using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Mmosoft.Oops.Colors;

namespace Mmosoft.Oops
{
    public partial class SmallNotification : Control
    {
        private bool isHovered;
        private NotifyType notifyType;

        // txt
        private Rectangle textRect;
        // image
        private Rectangle imageRect;
        private const int IMAGE_PADDING = 8;
        private Dictionary<NotifyType, Image> notifyImages = new Dictionary<NotifyType, Image>
        {
            { NotifyType.Information, SvgPath8x8Mgr.Get("M3.5 0h1v1h-1zM3.5 2h1v5h-1v-5z", 10, BrushCreator.CreateSolidBrush(SmallNofiticationColors.InformationText) ) },
            { NotifyType.Warning, SvgPath8x8Mgr.Get(SvgPathBx8Constants.Warning, 10, BrushCreator.CreateSolidBrush(SmallNofiticationColors.InformationText) ) },
            { NotifyType.Danger, SvgPath8x8Mgr.Get(SvgPathBx8Constants.CircleX, 10, BrushCreator.CreateSolidBrush(SmallNofiticationColors.InformationText) ) },
        };

        private Dictionary<NotifyType, Image> notifyHoverImages = new Dictionary<NotifyType, Image>
        {
            { NotifyType.Information, SvgPath8x8Mgr.Get("M3.5 0h1v1h-1zM3.5 2h1v5h-1v-5z", 10, BrushCreator.CreateSolidBrush(SmallNofiticationColors.InformationTextHovered) ) },
            { NotifyType.Warning, SvgPath8x8Mgr.Get(SvgPathBx8Constants.Warning, 10, BrushCreator.CreateSolidBrush(SmallNofiticationColors.InformationTextHovered) ) },
            { NotifyType.Danger, SvgPath8x8Mgr.Get(SvgPathBx8Constants.CircleX, 10, BrushCreator.CreateSolidBrush(SmallNofiticationColors.InformationTextHovered) ) },
        };

        Animation.Animator inAnim;
        Animation.Animator outAnim;

        public SmallNotification()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //
            Visible = DesignMode;

            Height = 45;
            Padding = new Padding(0);
            Margin = new Padding(0);
            
            InitAnimation();            
        }

        private void InitAnimation()
        {
            // Init in animation
            inAnim = new Animation.Animator();
            inAnim.Add(new Animation.Step
            {
                AnimAction = (i) =>
                {
                    this.Top = 10; //px
                    this.Left = this.Parent.Width;
                }
            });
            inAnim.Add(new Animation.Step
            {
                TotalStep = 10,
                Interval = 20,
                AnimAction = (i) => this.Left -= (int)Math.Round(this.Width / 9d)
            });            
            for (int i = 0, shakeTime = 3; i < shakeTime; i++)
            {
                var ii = i;
                inAnim.Add(new Animation.Step
                {
                    TotalStep = 1,
                    Interval = 20,
                    AnimAction = (e) => this.Left += (shakeTime - ii) * 2
                });
                inAnim.Add(new Animation.Step
                {
                    TotalStep = 1,
                    Interval = 20,
                    AnimAction = (e) => this.Left -= (shakeTime - ii) * 2
                });
            }

            // init out animation
            outAnim = new Animation.Animator();
            outAnim.Add(new Animation.Step()
            {
                TotalStep = 10,
                Interval = 20,
                AnimAction = (i) => this.Left += (int)Math.Round(this.Width / 9d)
            });
        }

        public void Notify(string message, NotifyType notifyType = NotifyType.Information)
        {
            if (!this.Visible)
                this.Visible = true;
            //
            this.notifyType = notifyType;
            Text = message;
            CalculateTextRect();
            //
            this.BringToFront();
            //
            inAnim.Stop();
            inAnim.Start();
        }
        
        private void CalculateTextRect()
        {
            int imageSize = this.Height - 2 * IMAGE_PADDING;
            imageRect = new Rectangle(IMAGE_PADDING, IMAGE_PADDING, imageSize, imageSize);

            Size txtSize = TextRenderer.MeasureText(this.Text, this.Font);            
            textRect = new Rectangle(imageRect.Right + IMAGE_PADDING, (this.Height - txtSize.Height) / 2, txtSize.Width, txtSize.Height);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);            
            CalculateTextRect();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            outAnim.Start();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            this.Cursor = Cursors.Hand;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            this.Cursor = Cursors.Default;
            Invalidate();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;            

            string bgColor = null;            
            string textColor = null;
            string borderColor = null;

            if (isHovered)
            {
                borderColor = SmallNofiticationColors.BorderHover;

                if (notifyType == NotifyType.Information)
                {
                    bgColor = SmallNofiticationColors.InformationBackgroundHovered;
                    textColor = SmallNofiticationColors.InformationTextHovered;
                }
                else if (notifyType == NotifyType.Warning)
                {
                    bgColor = SmallNofiticationColors.WarningBackgroundHovered;
                    textColor = SmallNofiticationColors.WarningTextHovered;
                }
                else // danger
                {
                    bgColor = SmallNofiticationColors.DangerBackgroundHovered;
                    textColor = SmallNofiticationColors.DangerTextHovered;
                }                
            }
            else
            {
                borderColor = SmallNofiticationColors.Border;
                if (notifyType == NotifyType.Information)
                {
                    bgColor = SmallNofiticationColors.InformationBackground;
                    textColor = SmallNofiticationColors.InformationText;
                }
                else if (notifyType == NotifyType.Warning)
                {
                    bgColor = SmallNofiticationColors.WarningBackground;
                    textColor = SmallNofiticationColors.WarningText;
                }
                else
                {
                    bgColor = SmallNofiticationColors.DangerBackground;
                    textColor = SmallNofiticationColors.DangerText;
                }                
            }

            // background
            g.FillRectangle(BrushCreator.CreateSolidBrush(bgColor), ClientRectangle);
            // border
            g.DrawRectangle(PenCreator.Create(borderColor), 
                new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
            // content
            g.DrawImage(isHovered? notifyHoverImages[notifyType] : notifyImages[notifyType], imageRect);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(this.Text, this.Font,
                BrushCreator.CreateSolidBrush(textColor),
                new Point(textRect.X, textRect.Y));
        }
    }
}
