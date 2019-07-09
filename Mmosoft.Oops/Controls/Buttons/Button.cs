using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Buttons
{
    [Serializable]
    public partial class Button : Control
    {
        // non-browsable fields
        private bool _isMouseHovered;
        private bool _isMouseDown;
        private SolidBrush _backgroundBrush;

        private SolidBrush _textBrush;
        private StringFormat _textFormat;

        private Rectangle DrawableRectangle
        {
            get { return new Rectangle(0, 0, this.Width - 1, this.Height - 1); }
        }
        private Pen _borderPen;       

        [Browsable(true)]
        [MergableProperty(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [Description("Define colors for button control")]
        [TypeConverter(typeof(ExpandableObjectConverter))]   
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonColors Colors { get { return _colors; } set { _colors = value; Invalidate(); } }
        private ButtonColors _colors;

        // 
        public Button()
        {
            _colors = new ButtonColors();

            // 
            _borderPen = PenCreator.Create(Colors.Border, 2f);
            _borderPen.Alignment = PenAlignment.Inset;
            _backgroundBrush = BrushCreator.CreateSolidBrush(_colors.Bg);

            _textBrush = BrushCreator.CreateSolidBrush(_colors.Text);
            _textFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        // Mouse stuff
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isMouseDown = true;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isMouseDown = false;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isMouseHovered = true;
            Cursor = Cursors.Hand;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isMouseHovered = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        // Change stuff
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }
        private void UpdateColors()
        {
            Color bgColor, txtColor, borderColor;

            if (!Enabled)
            {
                bgColor = Colors.BgDisabled;
                borderColor = Colors.BorderDisabled;
                txtColor = Colors.TextDisabled;
            }
            else if (_isMouseDown)
            {
                bgColor = Colors.BgFocused;
                borderColor = Colors.BorderFocused;
                txtColor = Colors.TextFocused;
            }
            else if (_isMouseHovered)
            {
                bgColor = Colors.BgHovered;
                borderColor = Colors.BorderHovered;
                txtColor = Colors.TextHovered;
            }
            else
            {
                bgColor = Colors.Bg;
                borderColor = Colors.Border;
                txtColor = Colors.Text;
            }

            _backgroundBrush.Color = bgColor;
            _textBrush.Color = txtColor;
            _borderPen.Color = borderColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;            
            
            UpdateColors();

            g.FillRectangle(_backgroundBrush, this.ClientRectangle);
            g.DrawRectangle(_borderPen, this.ClientRectangle);
            g.DrawString(Text, Font, _textBrush, ClientRectangle, _textFormat);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _borderPen.Dispose();
                _backgroundBrush.Dispose();

                _textBrush.Dispose();
                _textFormat.Dispose();
            }
        }
    }
}