using Mmosoft.Oops;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    public partial class frmBoxShadowDemo : Form
    {
        Mmosoft.Oops.Layers.SketchDrawer skecth;

        public frmBoxShadowDemo()
        {
            InitializeComponent();
            skecth = new Mmosoft.Oops.Layers.SketchDrawer(this) { StepX = 50, StepY = 50, Mirror = true };
        }

        private void frmIconMgrLab_Load(object sender, EventArgs e)
        {
            
        }
    }
}
