using Mmosoft.Oops;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mmosoft.OopsTest.SideBarDemo
{
    public partial class frmSingleLevelSideBar : Form
    {        
        public frmSingleLevelSideBar()
        {
            InitializeComponent();
            SetupNavBar();
            SetupTitleBar();
            btnCollapse.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowThickLeft, 2, Brushes.Black);
        }

        private void frmSingleLevelSideBar_Shown(object sender, EventArgs e)
        {
            navBar.MakeAcrylicBackground();
            SetupImageGrid();
        }

        private bool collapse;

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

            navBar.Top = 1;
            navBar.Left = 1;
            navBar.Width = 171;
            navBar.Height = 553;
            navBar.EnableAcrylicStyle = false;
            navBar.Initialize(home, bg, colors, lockScreen, themes, fonts, start, taskbar);
        }
        private void OnNavBarCollappsedHandler()
        {            
            panel2.Width = this.Width - panel2.Left;
        }
        private EventHandler ItemClickHandler(string msg)
        {
            return (s, e) => label1.Text = msg;
        }
        
        private void SetupTitleBar()
        {
            titleBar1.MinimizeEnable = true;
            titleBar1.MaximizeEnable = false;
            titleBar1.Text = "Demo sample";
            titleBar1.OnMouseDragCompleted += (s, e) => navBar.MakeAcrylicBackground();
            titleBar1.OnMouseDragging += (s, e) => 
            {
                this.Left += e.OffsetX;
                this.Top += e.OffsetY;
            };
            titleBar1.OnMinimizeClicked += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar1.OnMaximizeClicked += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Normal)
                    this.WindowState = FormWindowState.Maximized;
                else if (this.WindowState == FormWindowState.Maximized)
                    this.WindowState = FormWindowState.Normal;

            };
            titleBar1.OnCloseClicked += (s, e) => this.Close();
        }

        private void SetupImageGrid()
        {
            var imgPath = @"D:\Image\cgi";
            var images = new List<Image>();
            foreach (var item in Directory.EnumerateFiles(imgPath))
                images.Add(new Bitmap(item));
            imageGrid1.Column = 3;
            imageGrid1.ImagePadding = 2;
            imageGrid1.Load(images);
        }
    }
}
