using Mmosoft.Oops;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.OopsTest.SideBarDemo
{
    public partial class frmSingleLevelSideBar : Form
    {
        // form show hide control
        private bool _formShown = false;
        
        public frmSingleLevelSideBar()
        {
            InitializeComponent();
            SetupNavBar();
        }
        
        // navigation configuration
        private void SetupNavBar()
        {
            var home = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Home",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Home, 2, Brushes.Black),
                Clicked = ItemClickHandler("Home"),
            };

            var bg = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Background",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Monitor, 2, Brushes.Black),
                Clicked = ItemClickHandler("Background"),
            };

            var colors = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Colors",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Brush, 2, Brushes.Black),
                Clicked = ItemClickHandler("Colors"),
            };

            var lockScreen = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Lock screen",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.LockLocked, 2, Brushes.Black),
                Clicked = ItemClickHandler("Lock screen"),
            };

            var themes = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Themes",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Contrast, 2, Brushes.Black),
                Clicked = ItemClickHandler("Themes"),
            };

            var fonts = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Fonts",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Text, 2, Brushes.Black),
                Clicked = ItemClickHandler("Fonts"),
            };

            var start = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Start",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Box, 2, Brushes.Black),
                Clicked = ItemClickHandler("Start"),
            };

            var taskbar = new Mmosoft.Oops.SingleLevelNavBar.NavBarItem()
            {
                Text = "Taskbar",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.List, 2, Brushes.Black),
                Clicked = ItemClickHandler("Taskbar"),
            };
            
            navBar.Initialize(home, bg, colors, lockScreen, themes, fonts, start, taskbar);
        }
        private EventHandler ItemClickHandler(string msg)
        {
            return (s, e) => label1.Text = msg;
        }

        // form movement
        private bool mouseIsDown; // mouse down state
        private Point mouseDownLocation; // where mouse down
        private Point mouseLocation; // last mouse down + hold position
        private void pnHeader_MouseDown(object sender, MouseEventArgs e)
        {            
            mouseIsDown = true;
            mouseLocation = e.Location;
            mouseDownLocation = PointToScreen(e.Location);
        }
        private void pnHeader_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            if (mouseDownLocation != PointToScreen(e.Location))
                navBar.MakeAcrylicBackground();
        }
        private void pnHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                this.Left += e.Location.X - mouseLocation.X;
                this.Top += e.Location.Y - mouseLocation.Y;
            }
        }
        private void pnHeader_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        private void pnHeader_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        // max - normal size switch
        private void frmSingleLevelSideBar_Shown(object sender, EventArgs e)
        {
            _formShown = true;
            navBar.MakeAcrylicBackground();
        }

        // another demo
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void navBarSingle_Click(object sender, EventArgs e)
        {
            new SideBarDemo.frmSingleLevelSideBar().ShowDialog();
        }
        private void btnTable_Click(object sender, EventArgs e)
        {
            new frmTableDemo().ShowDialog();
        }
        private void btnSvgPath_Click(object sender, EventArgs e)
        {
            new frmSvgPathDemo().ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            new SideBarDemo.frmMultiLevelSideBar().ShowDialog();
        }
    }
}
