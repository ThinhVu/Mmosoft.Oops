using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.iosAppStoreItem
{
    public class iosAppStoreItemControl : Control
    {
        private int _padding = 10;
        private int _borderRadius = 30;
        private Rectangle _imageBoundary;
        private Rectangle _closeIconBoundary;

        private int _closeButtonPadding = 10;
        private int _closeButtonSize = 20;

        private bool _isOpenning;

        private Image _itemImage;
        public Image ItemImage 
        { 
            get 
            { return _itemImage; }
            set
            {
                _itemImage = value;
                CalculateItemImageSize();
                CalculateTextSize();
                Invalidate();
            }
        }

        private Size _contentSize;
        private Rectangle _contentBounds; // bound of content
        private TextFormatFlags _contentFormatFlag = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.PreserveGraphicsClipping;
        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                // Compute content size
                CalculateTextSize();
                if (!DesignMode)
                    Invalidate();
            }
        }
        
        //
        Animation.Animator _animatorIn;
        Animation.Animator _animatorOut;

        //
        public iosAppStoreItemControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;

            
            InitImageAnimator();
        }

        // init animator
        private void InitImageAnimator()
        {
            #region Init anim in
            _animatorIn = new Animation.Animator();
            // => smaller
            _animatorIn.Add(new Animation.Step
            {
                Interval = 12,
                TotalStep = 5,
                AnimAction = (stepI) =>
                {
                    _imageBoundary = _imageBoundary.AdjustSizeFromCenter(2, 2);
                    Invalidate();
                }
            });

            // => bigger
            _animatorIn.Add(new Animation.Step
            {
                Interval = 8,
                TotalStep = 15,
                AnimAction = (stepI) =>
                {
                    _borderRadius -= 2;
                    _imageBoundary = _imageBoundary.AdjustSizeFromCenter(2, 2);
                    Invalidate();
                }
            });
            _animatorIn.OnCompleted = () => { _isOpenning = true; Invalidate(); };
            #endregion

            #region init anim out
            // => smaller
            _animatorOut = new Animation.Animator();
            _animatorOut.Add(new Animation.Step
            {
                AnimAction = (i) => _isOpenning = false
            });

            _animatorOut.Add(new Animation.Step
            {
                Interval = 12,
                TotalStep = 15,
                AnimAction = (stepI) =>
                {
                    _borderRadius += 2;
                    _imageBoundary = _imageBoundary.AdjustSizeFromCenter(2, 2);
                    Invalidate();
                }
            });

            // bigger
            _animatorOut.Add(new Animation.Step
            {
                Interval = 24,
                TotalStep = 5,
                AnimAction = (stepI) =>
                {
                    _imageBoundary = _imageBoundary.AdjustSizeFromCenter(2, 2);
                    Invalidate();
                }
            });
            #endregion
        }

        // calculate image clipped region
        private GraphicsPath ComputeGraphisPath(Rectangle r)
        {
            int rectWH = _borderRadius * 2;
            
            var gp = new GraphicsPath();
            if (rectWH == 0)
            {
                gp.AddRectangle(r);
            }
            else
            {
                var rectTopLeft = new Rectangle(r.X, r.Y, rectWH, rectWH);
                var rectBotLeft = new Rectangle(r.X, r.Bottom - rectWH, rectWH, rectWH);
                var rectBotRight = new Rectangle(r.Right - rectWH, r.Bottom - rectWH, rectWH, rectWH);
                var rectTopRight = new Rectangle(r.Right - rectWH, r.Y, rectWH, rectWH);

                gp.AddArc(rectTopLeft, -90, -90);
                gp.AddArc(rectBotLeft, -180, -90);
                gp.AddArc(rectBotRight, -270, -90);
                gp.AddArc(rectTopRight, -360, -90);
            }
            return gp;

        }
        
        // size
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var r = this.ClientRectangle;
            // w1/h1 = w2/h2
            // h2 = w2 / (w1/h1)

            CalculateItemImageSize();
            CalculateTextSize();

            _closeIconBoundary = new Rectangle(this.Width - _closeButtonPadding - _closeButtonSize, _closeButtonPadding, _closeButtonSize, _closeButtonSize);
        }
        private void CalculateItemImageSize()
        {
            if (_itemImage != null)
            {
                // init size (10, 10 , w - 20, 300)
                float whRatio = 1f * _itemImage.Width / _itemImage.Height;
                _imageBoundary = new Rectangle(
                    _padding, _padding,
                    this.Width - 2 * _padding,
                    (int)(this.Width - 2 * _padding / whRatio));
            }
        }
        private void CalculateTextSize()
        {
            _contentSize = TextRenderer.MeasureText(
                _content, 
                this.Font, 
                new Size(this.Width - 2 * _padding, 1), 
                _contentFormatFlag);
            _contentBounds = new Rectangle(
                _padding,
                _imageBoundary.Bottom + _padding * 2 /*make bigger padding*/,
                _contentSize.Width,
                _contentSize.Height);
        }

        // handle when user hover mouse over close button
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = _closeIconBoundary.Contains(e.Location) && _isOpenning ? Cursors.Hand : Cursors.Default;
        }
        // mouse click handler
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnClick(e);

            // only handle if user click left click
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            if (_isOpenning)
            {
                // handle when user click to close button
                if (_closeIconBoundary.Contains(e.Location))
                {
                    _animatorOut.Start();
                }
            }
            else
            {
                _animatorIn.Start();
            }
        }

        //
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // using for design visual
            if (DesignMode)
            {
                _itemImage = Image.FromFile(@"D:\Image\cgi\noragami_by_guweiz-d9x26b7.jpg");
                this.Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            }

            // draw item image
            if (ItemImage != null)
            {
                g.SetClip(ComputeGraphisPath(_imageBoundary));
                g.DrawImage(ItemImage, _imageBoundary);
                g.SetClip(this.ClientRectangle);
            }

            if (_isOpenning)
            {
                g.FillEllipse(Brushes.Gray, _closeIconBoundary);
                g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.XThin, 10, Brushes.White), _closeIconBoundary.AdjustSize(-8, -8).AdjustXY(4, 4));

                if (this.Content != null)
                {
                    TextRenderer.DrawText(e.Graphics, this.Content, this.Font, _contentBounds, Color.Black, _contentFormatFlag);
                }
            }
        }
    }
}
