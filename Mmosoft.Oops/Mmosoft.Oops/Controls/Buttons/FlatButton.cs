using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Buttons
{
    public class FlatButton : Control
    {
        [Browsable(true)]
        private Image _image;
        public Image Image { get { return _image; } set { _image = value; Invalidate(); } }
        
        [Browsable(true)]
        public Color MouseEnterColor { get; set; }        

        private Rectangle _iconRect;
        private int _iconPadding;
        private int _iconSize;
        private SolidBrush _backBrush;        

        public FlatButton()
        {
            SetStyle(ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, false);
            this.Height = 40; // default
            _iconPadding = 12;
            _iconSize = 16;
            _iconRect = new Rectangle(_iconPadding, _iconPadding, _iconSize, _iconSize);
            _backBrush = BrushCreator.CreateSolidBrush();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _backBrush.Color = MouseEnterColor;
            this.Cursor = Cursors.Hand;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
                _backBrush.Color = Color.FromArgb(80, this.BackColor);
            else
                _backBrush.Color = Color.FromArgb(255, this.BackColor);

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _backBrush.Color = this.BackColor;
            this.Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _backBrush.Color = this.BackColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);           

            // background
            e.Graphics.FillRectangle(_backBrush, this.ClientRectangle);

            // image
            if (this.Image != null) e.Graphics.DrawImage(this.Image, _iconRect);

            // text
            var brText = new SolidBrush( Enabled? this.ForeColor : Color.FromArgb(80, this.ForeColor));
            e.Graphics.DrawString(this.Text, this.Font, brText, this.ClientRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            brText.Dispose();

            // border
            e.Graphics.DrawRectangle(Pens.Black, this.ClientRectangle.ChangeSizeRelative(-1, -1));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _backBrush.Dispose();
            }
        }
    }
}
