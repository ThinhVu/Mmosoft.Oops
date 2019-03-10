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
        }

        private void ConfigSideBar()
        {
            var home = new Mmosoft.Oops.NavBarItem()
            {
                Text = "Home",
                Clicked = ItemClickAct("Home"),
            };
            var management = new Mmosoft.Oops.NavBarItem()
            {
                Text = "Management",
                Clicked = ItemClickAct("Management"),
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
                Clicked = ItemClickAct("Report"),
                Items = new List<Mmosoft.Oops.NavBarItem>()
                {
                    new Mmosoft.Oops.NavBarItem() { Text = "Daily report", Clicked = ItemClickAct("Daily report") },
                    new Mmosoft.Oops.NavBarItem() { Text = "Monthly report", Clicked = ItemClickAct("Monthly report") },
                    new Mmosoft.Oops.NavBarItem() { Text = "Yearly report", Clicked = ItemClickAct("Yearly report") }
                }
            };
            var about = new Mmosoft.Oops.NavBarItem() { Text = "About", Clicked = ItemClickAct("About") };

            // side bar            
            sideBar1.IdentWidth = 20;
            sideBar1.Initialize(home, management, reporting, about);
        }

        private EventHandler ItemClickAct(string msg)
        {
            return (s, e) => label1.Text = msg;
        }

        private void animateControlSimpleImpl1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // form movement
        private bool down;
        private Point location;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.LocationChanged -= frmMultiLevelSideBar_LocationChanged;
            down = true;
            location = e.Location;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
            if (_shonw)
            {
                sideBar1.CaptureBackgroundImage();
                sideBar1.Invalidate();
            }
            this.LocationChanged += frmMultiLevelSideBar_LocationChanged;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
                this.Left += e.Location.X - location.X;
                this.Top += e.Location.Y - location.Y;
            }
        }
        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        // max - normal switch
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void frmMultiLevelSideBar_LocationChanged(object sender, EventArgs e)
        {
            this.LocationChanged -= frmMultiLevelSideBar_LocationChanged;

            if (_shonw)
            {
                sideBar1.CaptureBackgroundImage();
                sideBar1.Invalidate();
            }

            this.LocationChanged += frmMultiLevelSideBar_LocationChanged;
        }

        bool _shonw = false;
        private void frmMultiLevelSideBar_Shown(object sender, EventArgs e)
        {
            _shonw = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
