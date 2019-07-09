using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Mmosoft.Oops.Colors;
using Mmosoft.Oops.Controls.Notifications;

namespace Mmosoft.Oops
{
    public partial class Notification : Control
    {
        private const int PADDING = 10;
        private const int ICONSIZE = 25;
        private static Dictionary<NotifyType, Image> notifyImages = new Dictionary<NotifyType, Image>
        {
            { NotifyType.Information, SvgPath8x8Mgr.Get("M3.5 0h1v1h-1zM3.5 2h1v5h-1v-5z", 10, BrushCreator.CreateSolidBrush("255, 112, 245, 132") ) },
            { NotifyType.Warning, SvgPath8x8Mgr.Get(SvgPathBx8Constants.Warning, 10, BrushCreator.CreateSolidBrush("255, 255, 154, 0" )) },
            { NotifyType.Danger, SvgPath8x8Mgr.Get(SvgPathBx8Constants.CircleX, 10, BrushCreator.CreateSolidBrush("255, 255, 0, 0") ) },
        };

        private NotifyType _notifyType;
        private Animation.Animator _animateIn;
        private Animation.Animator _animateOut;
        private NotifyOut _outType;

        //
        private Rectangle _titleBounds;
        private Rectangle _textBounds;
        private Rectangle _iconBounds;

        // 
        public string Title;
        private Font _titleFont;
        public Action OnCompeleted;

        //
        internal Notification(NotifyType notifyType, NotifyOut notifyOutType, int initialWidth)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            

            this.Width = initialWidth;
            //
            _notifyType = notifyType;
            _outType = notifyOutType;

            InitAnimation();
        }

        //
        private void InitAnimation()
        {
            _animateIn = new Animation.Animator();
            // 
            _animateIn.OnCompleted = () => 
            { 
                if (_outType == NotifyOut.Automatically) 
                    _animateOut.Start();
            };
            // 
            _animateIn.Add(new Animation.Step
            {
                TotalStep = 11,
                Interval = 20,
                AnimAction = (i) => this.Left -= (int)Math.Round(this.Width / 10d)
            });
            for (int i = 0, shakeTime = 3; i < shakeTime; i++)
            {
                var ii = i;
                _animateIn.Add(new Animation.Step
                {
                    TotalStep = 1,
                    Interval = 20,
                    AnimAction = (e) => this.Left += (shakeTime - ii) * 2
                });
                _animateIn.Add(new Animation.Step
                {
                    TotalStep = 1,
                    Interval = 20,
                    AnimAction = (e) => this.Left -= (shakeTime - ii) * 2
                });
            }
            _animateIn.Wait(2000);

            // out
            _animateOut = new Animation.Animator();
            _animateOut.Add(new Animation.Step()
            {
                TotalStep = 10,
                Interval = 20,
                AnimAction = (i) => this.Left += (int)Math.Round(this.Width / 10d)
            });
            _animateOut.OnCompleted = () =>
            { 
                if (this.OnCompeleted != null)
                    this.OnCompeleted();
            };
        }
        private void CalculateBounds()
        {
            Graphics g = this.CreateGraphics();

            _titleFont = new Font(this.Font, FontStyle.Bold);
            _iconBounds = new Rectangle(PADDING, PADDING, ICONSIZE, ICONSIZE);

            SizeF titleSize = TextRenderer.MeasureText(this.Title, _titleFont);

            int availableTextWidth = this.Width - ICONSIZE - PADDING * 3;
            SizeF txtSize = TextRenderer.MeasureText(this.Text, this.Font, new Size(availableTextWidth, 30), TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);
            
            float textWidth = txtSize.Width;
            float lineHeight = txtSize.Height;
            while (textWidth > availableTextWidth)
            {
                txtSize.Height += lineHeight;
                textWidth -= availableTextWidth;
            }
            this.Height = (int)(PADDING + titleSize.Height + txtSize.Height + PADDING);
            _titleBounds = new Rectangle(
                _iconBounds.Right + PADDING,
                PADDING,
                (int)titleSize.Width,
                (int)titleSize.Height);
            _textBounds = new Rectangle(
                _iconBounds.Right + PADDING,
                _titleBounds.Bottom,
                availableTextWidth,
                (int)txtSize.Height);

        }

        //
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_outType == NotifyOut.Manually)
                _animateOut.Start();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (_outType == NotifyOut.Manually)
                this.Cursor = Cursors.Hand;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_outType == NotifyOut.Manually)
                this.Cursor = Cursors.Default;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            string bgColor = null;
            string textColor = null;
            string borderColor = null;

            borderColor = SmallNofiticationColors.Border;
            if (_notifyType == NotifyType.Information)
            {
                bgColor = SmallNofiticationColors.InformationBackground;
                textColor = SmallNofiticationColors.InformationText;
            }
            else if (_notifyType == NotifyType.Warning)
            {
                bgColor = SmallNofiticationColors.WarningBackground;
                textColor = SmallNofiticationColors.WarningText;
            }
            else
            {
                bgColor = SmallNofiticationColors.DangerBackground;
                textColor = SmallNofiticationColors.DangerText;
            }

            // background
            g.FillRectangle(BrushCreator.CreateSolidBrush(bgColor), ClientRectangle);
            // border
            g.DrawRectangle(PenCreator.Create(borderColor),
                new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
            // content
            g.DrawImage(notifyImages[_notifyType], _iconBounds);
            TextRenderer.DrawText(e.Graphics, this.Title, _titleFont, _titleBounds, ExColorTranslator.Get(textColor), TextFormatFlags.NoPadding);
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, _textBounds, ExColorTranslator.Get(textColor), TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);
        }

        //
        public void Start()
        {
            CalculateBounds();
            _animateIn.Start();
        }
    }
}
