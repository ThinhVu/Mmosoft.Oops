using System;
using System.Windows.Forms;

namespace Mmosoft.OopsTest.SideBarDemo
{
    public partial class frmSideBarExample : Form
    {
        public frmSideBarExample()
        {
            InitializeComponent();
        }

        private void ConfigSideBar2()
        {
            var home = new Mmosoft.Oops.NavBarItem() { Text = "HOME", };
            var management = new Mmosoft.Oops.NavBarItem() { Text = "MANAGEMENT" };
            var reporting = new Mmosoft.Oops.NavBarItem() { Text = "REPORT" };
            var about = new Mmosoft.Oops.NavBarItem() { Text = "ABOUT" };

            // side bar
            sideBar2.IdentWidth = 0;
            sideBar2.Initialize(home, management, reporting, about);
            sideBar2.MultiLevel = false;
        }

        private void frmSideBarExample_Load(object sender, EventArgs e)
        {
            ConfigSideBar2();
        }
    }
}
