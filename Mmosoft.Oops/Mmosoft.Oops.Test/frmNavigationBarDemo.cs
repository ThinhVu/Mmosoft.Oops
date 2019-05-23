using Mmosoft.Oops;
using Mmosoft.Oops.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    public partial class frmNavigationBarDemo : Form
    {        
        public frmNavigationBarDemo()
        {
            InitializeComponent();
            SetupNavBar();
            SetupTitleBar();
            btnCollapse.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ArrowThickLeft, 2, Brushes.Black);
        }

        private void frmSingleLevelSideBar_Shown(object sender, EventArgs e)
        {
            SetupImageGrid();
            imageGrid1.AutoScrollToSelectedImage = true;
        }
        private void SetupTitleBar()
        {
            titleBar1.MinimizeEnable = true;
            titleBar1.MaximizeEnable = true;
            titleBar1.Text = "Demo sample";
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
        private void SetupNavBar()
        {
            #region setup menu position
            navBar.Top = 1;
            navBar.Left = 1;
            navBar.Width = 234;
            navBar.Height = 553;
            #endregion

            #region Setup menu item
            Func<string, EventHandler> itemClick = (msg) => (s, e) =>label1.Text = msg;
            var home = new NavBarItem()
            {
                Text = "Home",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Home, 4, Brushes.Black),
                Clicked = itemClick("Home"),
            };

            var bg = new NavBarItem()
            {
                Text = "Background",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Monitor, 4, Brushes.Black),
                Clicked = itemClick("Background"),
                Items = new List<NavBarItem>
                {
                    new NavBarItem()
                    {
                        Text = "Colors",
                        Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Brush, 4, Brushes.Black),
                        Clicked = itemClick("Colors"),
                    },
                    new NavBarItem()
                    {
                        Text = "Lock screen",
                        Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.LockLocked, 4, Brushes.Black),
                        Clicked = itemClick("Lock screen"),
                    },
                    new NavBarItem()
                    {
                        Text = "Themes",
                        Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Contrast, 4, Brushes.Black),
                        Clicked = itemClick("Themes"),
                    }
                }
            };
                        
            var fonts = new NavBarItem()
            {
                Text = "Fonts",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Text, 4, Brushes.Black),
                Clicked = itemClick("Fonts"),
            };

            var start = new NavBarItem()
            {
                Text = "Start",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Box, 4, Brushes.Black),
                Clicked = itemClick("Start"),
            };

            var taskbar = new NavBarItem()
            {
                Text = "Taskbar",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.List, 4, Brushes.Black),
                Clicked = itemClick("Taskbar"),
            };
           
            navBar.Initialize(home, bg, fonts, start, taskbar);
            #endregion

            // nav effect
            navBar.EnableHighlightReveal = true;
            // navBar.EnableAcrylicStyle = true;
        }                
        private void SetupImageGrid()
        {
            // setup layout
            imageGrid1.Column = 5;
            imageGrid1.Gutter = 2;

            #region Load images
            var imgPath = @"..\..\assests\images";
            var images = new List<Image>();
            foreach (var item in Directory.EnumerateFiles(imgPath))
                imageGrid1.Add(new Bitmap(item));
            
            #endregion

            imageGrid1.OnItemClicked += (s, e) => { navBar.BackgroundImage = e.Image; /*Do stuff*/ };
        }

        private void mediaController1_Load(object sender, EventArgs e)
        {

        }
    }
}
