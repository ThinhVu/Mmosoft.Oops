using Mmosoft.Oops;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.OopsTest.SideBarDemo
{
    public partial class frmMultiLevelSideBar : Form
    {
        public frmMultiLevelSideBar()
        {
            InitializeComponent();
            ConfigSideBar();
            SetupTitleBar();
        }

        private void ConfigSideBar()
        {
            var home = new Mmosoft.Oops.NavBarItem()
            {
                Text = "Home",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Home, 4, Brushes.Black),
                Clicked = ItemClickAct("Home"),
            };
            var management = new Mmosoft.Oops.NavBarItem()
            {
                Text = "Management",
                Clicked = ItemClickAct("Management"),
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Monitor, 4, Brushes.Black),
                Items = new List<Mmosoft.Oops.NavBarItem>()
                {
                    new Mmosoft.Oops.NavBarItem()
                    {
                        Text = "Customer management",
                        Clicked = ItemClickAct("Customer management"),
                        Items = new List<Mmosoft.Oops.NavBarItem>
                        {
                            new Mmosoft.Oops.NavBarItem{ Text = "Add customer info", Clicked = ItemClickAct("Add customer info"), },
                            new Mmosoft.Oops.NavBarItem{ Text = "Edit customer info", Clicked = ItemClickAct("Edit customer info"), },
                            new Mmosoft.Oops.NavBarItem{ Text = "Delele customer info", Clicked = ItemClickAct("Delele customer info"), }
                        }
                    },
                    new Mmosoft.Oops.NavBarItem()
                    {
                        Text = "Product management",
                        Clicked = ItemClickAct("Product management"),
                        Items = new List<Mmosoft.Oops.NavBarItem>
                        {
                            new Mmosoft.Oops.NavBarItem{ Text = "Add product info", Clicked = ItemClickAct("Add product info") },
                            new Mmosoft.Oops.NavBarItem{ Text = "Edit product info", Clicked = ItemClickAct("Edit product info") },
                            new Mmosoft.Oops.NavBarItem{ Text = "Delete product info", Clicked = ItemClickAct("Delete product info") }
                        }
                    },
                    new Mmosoft.Oops.NavBarItem() { Text = "Transaction Management", Clicked = ItemClickAct("Transaction Management") },
                }
            };
            var reporting = new Mmosoft.Oops.NavBarItem()
            {
                Text = "Report",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Brush, 4, Brushes.Black),
                Clicked = ItemClickAct("Report"),
                Items = new List<Mmosoft.Oops.NavBarItem>()
                {
                    new Mmosoft.Oops.NavBarItem() { Text = "Daily report", Clicked = ItemClickAct("Daily report") },
                    new Mmosoft.Oops.NavBarItem() { Text = "Monthly report", Clicked = ItemClickAct("Monthly report") },
                    new Mmosoft.Oops.NavBarItem() { Text = "Yearly report", Clicked = ItemClickAct("Yearly report") }
                }
            };
            var about = new Mmosoft.Oops.NavBarItem() 
            { 
                Text = "About",
                Icon = SvgPath8x8Mgr.Get(SvgPathBx8Constants.LockLocked, 4, Brushes.Black),
                Clicked = ItemClickAct("About") 
            };

            sideBar1.EnableHighlightReveal = true;
            sideBar1.Initialize(home, management, reporting, about);
        }

        private EventHandler ItemClickAct(string msg)
        {
            return (s, e) => label1.Text = msg;
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
    }
}
