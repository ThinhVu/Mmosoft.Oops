using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Mmosoft.Oops.Layers
{
    // Designer automatically tries to serialize all public UserControl properties.
    // If the property is not needed for our custom UserControl design time support
    // then we can Add "DesignerSerializationVisibility" attribute:
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 

    // Another way is change default property to public field
    [Serializable]
    public class LayerControl : Control
    {
        private Point txtPoint;
        public List<Layers.BaseDrawer> PreLayers;
        public List<Layers.BaseDrawer> PostLayers;
        
        public event EventHandler Clicked;

        public LayerControl()
        {
            PreLayers = new List<Layers.BaseDrawer> { };
            PostLayers = new List<Layers.BaseDrawer> { };
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (Clicked != null)
                Clicked(this, e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;
            // Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
            // Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            CalculateTextPoint();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            CalculateTextPoint();
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalculateTextPoint();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {            
            base.OnPaint(e);
            var g = e.Graphics;
            if (this.BackColor != Color.Transparent)
                g.Clear(this.BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var drawAbleSize = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            if (PreLayers != null && PreLayers.Count > 0)
            {                
                foreach (var drawer in PreLayers)
                {
                    if (drawer != null)
                    {
                        drawer.IsDesignMode = DesignMode;
                        drawer.IsHovered = false;
                        drawer.Draw(g, drawAbleSize);
                    }
                }
            }

            if (PostLayers != null && PostLayers.Count > 0)
            {
                foreach (var drawer in PostLayers)
                {
                    if (drawer != null)
                    {
                        drawer.IsDesignMode = DesignMode;
                        drawer.IsHovered = false;
                        drawer.Draw(g, drawAbleSize);
                    }
                }
            }

            g.DrawString(this.Text, this.Font, BrushCreator.CreateSolidBrush("#0"), txtPoint);
        }

        private void CalculateTextPoint()
        {
            var size = TextRenderer.MeasureText(this.Text, this.Font);
            txtPoint = new Point((this.Width - size.Width) / 2, (this.Height - size.Height) / 2);
        }
    }
}