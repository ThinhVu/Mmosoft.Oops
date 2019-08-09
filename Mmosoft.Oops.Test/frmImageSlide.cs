using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Test
{
    public partial class frmImageSlide : Form
    {
        public frmImageSlide()
        {
            InitializeComponent();
        }

        private void frmImageSlide_Load(object sender, EventArgs e)
        {
            var imgPath = @"D:\Apps\XiurenViewer\saved\[MFStar] Vol.177 诱惑大胸小太妹徐cake火红热辣制服魅力养眼私拍 30P\img";  // @"..\..\assests\images\imagegrid\"; 
            var images = new List<Image>();
            foreach (var item in Directory.EnumerateFiles(imgPath))
                imageSlide1.AddImage(new Bitmap(item));
            // imageSlide1.AddImage(images);
        }
    }
}




// @"D:\Apps\XiurenViewer\saved\[MFStar] Vol.177 诱惑大胸小太妹徐cake火红热辣制服魅力养眼私拍 30P\img"; 