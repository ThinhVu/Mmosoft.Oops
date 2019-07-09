using Mmosoft.Oops.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mmosoft.Oops.Test
{
    public partial class frmStackImageGridDemo : Form
    {
        public frmStackImageGridDemo()
        {
            InitializeComponent();
        }

        private void InitImageGridStyle()
        {
            btnApplyStyle.PerformClick();
        }
        private void SetupImageGrid()
        {
            #region Load images
            var imgPath = @"..\..\assests\images\imagegrid\";
            var images = new List<Image>();
            foreach (var item in Directory.EnumerateFiles(imgPath))
                imageGrid1.Add(new Bitmap(item));
            #endregion
        }

        private void frmImageGridDemo_Shown(object sender, EventArgs e)
        {
            InitImageGridStyle();
            SetupImageGrid();
        }

        private void btnApplyStyle_Click(object sender, EventArgs e)
        {
            imageGrid1.Column = (int)nudColumn.Value;
            imageGrid1.Gutter = (int)nudGutter.Value;
        }
    }
}
