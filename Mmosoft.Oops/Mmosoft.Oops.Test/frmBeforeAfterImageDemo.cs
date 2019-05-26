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
    public partial class frmBeforeAfterImageDemo : Form
    {
        public frmBeforeAfterImageDemo()
        {
            InitializeComponent();
            beforeAfterImage1.Before = Image.FromFile(@"..\..\assests\images\beforeafterimage\sample.jpg");
            beforeAfterImage1.After = Image.FromFile(@"..\..\assests\images\beforeafterimage\sample1.jpg");
        }
    }
}
