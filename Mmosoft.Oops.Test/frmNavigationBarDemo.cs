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
        }

        private void frmSingleLevelSideBar_Shown(object sender, EventArgs e)
        {
            SetupNavBar();
        }
        
        private void SetupNavBar()
        {
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

            navBar.OnCollapseExpandStateChanged += (s, e) => 
            {
                int widthChanged = navBar.ExpanedWidth - navBar.CollapsedWidth;
                if (!navBar.IsCollapsing)
                    widthChanged = -widthChanged;

                pnContent.Width += widthChanged;
                pnContent.Left = navBar.Right;
            };
        }

        private void btnApplyStyle_Click(object sender, EventArgs e)
        {
            navBar.SuspendLayout();
            navBar.ItemHeight = (int) nudItemHeight.Value;
            navBar.ItemIconSize = (int)nudIconSize.Value;
            navBar.IdentWidth = (int)nudIdentWidth.Value;
            navBar.DropDownSize = (int)nudDropdownSize.Value;
            navBar.CollapseExpandEnable = togCollapseExpandEnable.Checked;
            navBar.EnableHighlightReveal = togHighlightRevealEnabled.Checked;
            navBar.ResumeLayout();
        }
    }
}
