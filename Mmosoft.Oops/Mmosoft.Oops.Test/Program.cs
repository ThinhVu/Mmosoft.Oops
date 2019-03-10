using Mmosoft.Oops;
using Mmosoft.Oops.Test;
using Mmosoft.OopsTest.SideBarDemo;
using System;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSingleLevelSideBar());
        }
    }
}
