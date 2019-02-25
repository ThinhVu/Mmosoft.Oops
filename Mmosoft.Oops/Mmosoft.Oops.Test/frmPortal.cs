using System;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    public partial class frmPortal : Form
    {
        public frmPortal()
        {
            InitializeComponent();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmLayerControlDemo().ShowDialog();
        }

        private void btnBoxShadow_Click(object sender, EventArgs e)
        {
            new frmBoxShadowDemo().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new frmControlsDemo().ShowDialog();
        }

        private void btnProgressBar_Click(object sender, EventArgs e)
        {            
        }

        private void navBarSingle_Click(object sender, EventArgs e)
        {
            new SideBarDemo.frmSideBarExample().ShowDialog();
        }

        private void btnNavBarMulti_Click(object sender, EventArgs e)
        {
            new SideBarDemo.frmMultiLevelSideBar().ShowDialog();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            new frmTableDemo().ShowDialog();
        }

        private void btnSvgPath_Click(object sender, EventArgs e)
        {
            new frmSvgPathDemo().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
