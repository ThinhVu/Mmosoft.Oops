using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Buttons
{
    // TODO: Remove jagged border if border width > 1.
    // (How to apply antialiasing in this case)
    [Serializable]
    public partial class BorderRadiusButton : Control
    {
        // non-browsable fields
        private bool _isMouseHovered;
        private SolidBrush _backgroundBrush;

        private SolidBrush _textBrush;
        private StringFormat _textFormat;

        private Pen _borderPen;

        private Matrix moveMatrix;        
        private GraphicsPath _graphicsPath;        

        // prop
        [Browsable(true)]
        [MergableProperty(true)]
        [Category("Appearance")]
        [Description("Define border radius for button control")]
        public BorderRadius BorderRadius
        {
            get { return _borderRadius; }
            set {
                _borderRadius = value;                
                UpdateRegion();
                UpdateClientRegion();
                Invalidate();
            }
        }
        private BorderRadius _borderRadius;        
        
        [Browsable(true)]
        [MergableProperty(true)]        
        [Localizable(false)]
        [Category("Appearance")]
        [Description("Define colors for button control")]
        [TypeConverter(typeof(ExpandableObjectConverter))]   
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BorderRaidusButtonColors Colors { get { return _colors; } set { _colors = value; Invalidate(); } }
        private BorderRaidusButtonColors _colors;

        // 
        public BorderRadiusButton()
        {
            _colors = new BorderRaidusButtonColors();
            _borderRadius = new BorderRadius();

            // 
            _backgroundBrush = BrushCreator.CreateSolidBrush(_colors.Bg);

            _textBrush = BrushCreator.CreateSolidBrush(_colors.Text);
            _textFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            _borderPen = PenCreator.Create(_colors.Border, 1f);
            _borderPen.Alignment = PenAlignment.Inset;

            moveMatrix = new Matrix();
            moveMatrix.Translate(1, 1);

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        // Mouse stuff
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
            UpdateRegion();
            UpdateClientRegion();
            Invalidate();
        }

        //
        private static BorderRadius AdjustBorderRadius(int width, int height, BorderRadius borderRadius)
        {
            float brTop = borderRadius.TopLeft + borderRadius.TopRight;
            float brLeft = borderRadius.BottomLeft + borderRadius.BottomLeft;
            float brBot = borderRadius.TopLeft + borderRadius.BottomRight;
            float brRight = borderRadius.TopRight + borderRadius.BottomRight;

            float brTopRatio = width < brTop ? width / brTop /*reduce*/ : 1 /*keep the same*/;
            float brRightRatio = height < brRight ? height / brRight : 1;
            float brBotRatio = width < brBot ? width / brBot : 1;
            float brLeftRatio = height < brLeft ? height / brLeft : 1;

            float minRatio = Math.Min(brTopRatio, Math.Min(brRightRatio, Math.Min(brBotRatio, brLeftRatio)));

            var adjBr = new BorderRadius(
                (int)(borderRadius.TopLeft * minRatio),
                (int)(borderRadius.TopRight * minRatio),
                (int)(borderRadius.BottomLeft * minRatio),
                (int)(borderRadius.BottomRight * minRatio));
            return adjBr;
        }            
        private void UpdateRegion()
        {
            this.Region = new Region(CreateGraphicsPath(this.Width, this.Height, this.BorderRadius));
        }
        private void UpdateClientRegion()
        {
            _graphicsPath = CreateGraphicsPath(this.Width - 2, this.Height - 2, this.BorderRadius);
            _graphicsPath.Transform(moveMatrix);
        }
        private static GraphicsPath CreateGraphicsPath(int width, int height, BorderRadius borderRadius)
        {
            // adjust border radius
            BorderRadius br = AdjustBorderRadius(width, height, borderRadius);
            int topLeft = br.TopLeft;
            int topRight = br.TopRight;
            int botRight = br.BottomRight;
            int botLeft = br.BottomLeft;

            // Update graphic path base on border radius value
            // https://stackoverflow.com/questions/1734745/how-to-create-circle-with-b%C3%A9zier-curves
            float bezierRatio = (float)(4 * (Math.Sqrt(2) - 1) / 3);
            float invertRatio = 1 - bezierRatio;
            int avW = width - 1;
            int avH = height - 1;

            GraphicsPath gPath = new GraphicsPath();
            // top left
            if (topLeft != 0)
                gPath.AddBezier(
                    new PointF(topLeft, 0), // p1
                    new PointF(topLeft * invertRatio, 0), // control 1
                    new PointF(0, topLeft * invertRatio), // control 2
                    new PointF(0, topLeft)); // p2

            gPath.AddLine(new Point(0, topLeft), new Point(0, avH - botLeft));

            // bot left
            if (botLeft != 0)
                gPath.AddBezier(
                    new PointF(0, avH - botLeft), 
                    new PointF(0, avH - botLeft * invertRatio),
                    new PointF(botLeft * invertRatio, avH), 
                    new PointF(botLeft, avH));

            gPath.AddLine(new Point(botLeft, avH), new Point(avW - botRight, avH));

            // bot right
            if (botRight != 0)
                gPath.AddBezier(
                    new PointF(avW - botRight, avH), 
                    new PointF(avW - botRight * invertRatio, avH), 
                    new PointF(avW, avH - botRight * invertRatio), 
                    new PointF(avW, avH - botRight));

            gPath.AddLine(new Point(avW, avH - botRight), new Point(avW, topRight));

            // top right
            if (topRight != 0)
                gPath.AddBezier(
                    new PointF(avW, topRight), 
                    new PointF(avW, topRight * invertRatio), 
                    new PointF(avW - topRight * invertRatio, 0), 
                    new PointF(avW - topRight, 0));

            gPath.AddLine(new Point(avW - topRight, 0), new Point(topLeft, 0));
            gPath.CloseAllFigures();

            return gPath;
        }        
        private void UpdateColors()
        {
            Color bgColor, txtColor, borderColor;

            if (!Enabled)
            {
                bgColor = Colors.BgDisabled;
                txtColor = Colors.TextDisabled;
                borderColor = Colors.BorderDisabled;
            }
            else if (_isMouseHovered)
            {
                bgColor = Colors.BgHovered;
                txtColor = Colors.TextHovered;
                borderColor = Colors.BorderHovered;
            }
            else
            {
                bgColor = Colors.Bg;
                txtColor = Colors.Text;
                borderColor = Colors.Border;
            }

            _backgroundBrush.Color = bgColor;
            _textBrush.Color = txtColor;
            _borderPen.Color = borderColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            
            UpdateColors();

            if (_backgroundBrush.Color.A != 0)
                g.FillPath(_backgroundBrush, _graphicsPath);
            g.DrawPath(_borderPen, _graphicsPath);
            g.DrawString(Text, Font, _textBrush, ClientRectangle, _textFormat);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _backgroundBrush.Dispose();

                _textBrush.Dispose();
                _textFormat.Dispose();

                _borderPen.Dispose();

                _graphicsPath.Dispose();
            }
        }
    }
}