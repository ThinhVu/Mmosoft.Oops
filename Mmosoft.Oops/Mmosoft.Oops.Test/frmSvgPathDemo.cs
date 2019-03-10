using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mmosoft.Oops;
using System.Drawing.Drawing2D;

namespace Mmosoft.OopsTest
{
    public partial class frmSvgPathDemo : Form
    {
        public frmSvgPathDemo()
        {
            InitializeComponent();
        }

        private void frmSvgPath_Load(object sender, EventArgs e)
        {            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            // draw 3s aperture
            g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.Aperture, 10, BrushCreator.CreateSolidBrush("#A")), new PointF(10, 10));
            g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.Aperture, 10, BrushCreator.CreateSolidBrush("#F")), new PointF(8, 8));
            // draw 3d battery empty 
            g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.BatteryEmpty, 8, BrushCreator.CreateSolidBrush("#A")), new PointF(100, 100));
            g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.BatteryEmpty, 8, BrushCreator.CreateSolidBrush("#F")), new PointF(98, 98));
            // draw flat battery full
            g.DrawImage(SvgPath8x8Mgr.Get(SvgPathBx8Constants.BatteryFull, 5, BrushCreator.CreateSolidBrush("#80")), new PointF(100, 200));
        }
    }
}
