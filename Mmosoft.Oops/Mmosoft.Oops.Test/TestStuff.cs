using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Test
{
    public partial class TestStuff : Form
    {
        public TestStuff()
        {
            InitializeComponent();                   
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Pen p = new Pen(Color.Black, 3);
            p.Alignment = PenAlignment.Inset;                        

            e.Graphics.DrawPath(p, path());
        }

        private GraphicsPath path()
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(new Rectangle(0, 0, 40, 40), -90, -90);

            gp.AddArc(new Rectangle(0, 30, 40, 40), 180, -90);

            gp.AddArc(new Rectangle(100, 30, 40, 40), 90, -90);

            gp.AddArc(new Rectangle(100, 0, 40, 40), 0, -90);

            gp.CloseFigure();

            return gp;
        }
    }
}
