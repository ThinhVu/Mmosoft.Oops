using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Test
{
    public partial class frmIosAppStoreItemDemo : Form
    {
        public frmIosAppStoreItemDemo()
        {
            InitializeComponent();
        }

        private void frmIosAppStoreItemDemo_Load(object sender, EventArgs e)
        {
            iosAppStoreItemControl1.ItemImage = Image.FromFile(@"D:\Image\cgi\noragami_by_guweiz-d9x26b7.jpg");
            iosAppStoreItemControl1.Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        }
    }
}
