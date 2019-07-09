using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Buttons
{
    public class FlatButton : Control
    {
        // data
        private Image _iconImage;

        // ui
        private Rectangle _iconRect;
        private int _iconPadding;
        private int _iconSize;
        
        // resource
        private Pen _borderPen;
        private bool _showBorder;

        [Browsable(true)]
        public Image IconImage { get { return _iconImage; } set { _iconImage = value; Invalidate(); } }

        [Browsable(true)]
        public bool ShowBorder
        {
            get
            {
                return _showBorder;
            }
            set
            {
                _showBorder = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color BorderColor { get { return _borderPen.Color; } set { _borderPen.Color = value; Invalidate(); } }

        public FlatButton()
        {
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Height = 40;

            _iconPadding = 12;
            _iconSize = 16;
            _iconRect = new Rectangle(_iconPadding, _iconPadding, _iconSize, _iconSize);
            _borderPen = PenCreator.Create(Color.Black);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // image
            if (this.IconImage != null) e.Graphics.DrawImage(this.IconImage, _iconRect);

            // text
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                var brText = new SolidBrush(Enabled ? this.ForeColor : Color.FromArgb(80, this.ForeColor));
                e.Graphics.DrawString(this.Text, this.Font, brText, this.ClientRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                brText.Dispose();
            }
            
            // border
            if (ShowBorder)
                e.Graphics.DrawRectangle(_borderPen, this.ClientRectangle.IncreaseSize(-1, -1));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
