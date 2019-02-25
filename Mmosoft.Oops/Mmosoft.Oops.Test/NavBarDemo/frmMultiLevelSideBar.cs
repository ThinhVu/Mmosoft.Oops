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
                Text = "HOME",
                Clicked = ItemClickAct("Home"),
            };
            var management = new Mmosoft.Oops.NavBarItem()
            {
                Text = "MANAGEMENT",
                Clicked = ItemClickAct("Management"),
                Items = new List<Mmosoft.Oops.NavBarItem>()
                {
                    new Mmosoft.Oops.NavBarItem()
                    {
                        Text = "CUSTOMER MANAGEMENT",
                        Clicked = ItemClickAct("Customer management"),
                        Items = new List<Mmosoft.Oops.NavBarItem>
                        {
                            new Mmosoft.Oops.NavBarItem{ Text = "ADD CUSTOMER INFO", Clicked = ItemClickAct("Add customer info"), },
                            new Mmosoft.Oops.NavBarItem{ Text = "EDIT CUSTOMER INFO", Clicked = ItemClickAct("Edit customer info"), },
                            new Mmosoft.Oops.NavBarItem{ Text = "DELETE CUSTOMER INFO", Clicked = ItemClickAct("Delele customer info"), }
                        }
                    },
                    new Mmosoft.Oops.NavBarItem()
                    {
                        Text = "PRODUCT MANAGEMENT",
                        Clicked = ItemClickAct("Product management"),
                        Items = new List<Mmosoft.Oops.NavBarItem>
                        {
                            new Mmosoft.Oops.NavBarItem{ Text = "ADD PRODUCT INFO", Clicked = ItemClickAct("Add product info") },
                            new Mmosoft.Oops.NavBarItem{ Text = "EDIT PRODUCT INFO", Clicked = ItemClickAct("Edit product info") },
                            new Mmosoft.Oops.NavBarItem{ Text = "DELETE PRODUCT INFO", Clicked = ItemClickAct("Delete product info") }
                        }
                    },
                    new Mmosoft.Oops.NavBarItem() { Text = "TRANSACTION MANAGEMENT", Clicked = ItemClickAct("Transaction Management") },
                }
            };
            var reporting = new Mmosoft.Oops.NavBarItem()
            {
                Text = "REPORT",
                Clicked = ItemClickAct("Report"),
                Items = new List<Mmosoft.Oops.NavBarItem>()
                {
                    new Mmosoft.Oops.NavBarItem() { Text = "DAILY REPORT", Clicked = ItemClickAct("Daily report") },
                    new Mmosoft.Oops.NavBarItem() { Text = "MONTHLY REPORT", Clicked = ItemClickAct("Monthly report") },
                    new Mmosoft.Oops.NavBarItem() { Text = "YEARLY REPORT", Clicked = ItemClickAct("Yearly report") }
                }
            };
            var about = new Mmosoft.Oops.NavBarItem() { Text = "ABOUT", Clicked = ItemClickAct("About") };

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
            down = true;
            location = e.Location;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
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
    }
}
