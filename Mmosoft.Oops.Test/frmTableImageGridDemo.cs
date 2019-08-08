using Mmosoft.Oops.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mmosoft.Oops.Test
{
    public partial class frmTableImageGridDemo : Form
    {
        public frmTableImageGridDemo()
        {
            InitializeComponent();
        }

        private void InitImageGridStyle()
        {
            // swich mode to Fill to top
            cbDisplayMode.SelectedIndex = 0;
            cbMergeColumn.SelectedIndex = 0;

            btnApplyStyle.PerformClick();
        }
        private void SetupImageGrid()
        {
            #region Load images
            var imgPath = @"..\..\assests\images\imagegrid\";
            var images = new List<Image>();
            foreach (var item in Directory.EnumerateFiles(imgPath))
            {
                imageGrid1.Add(new Bitmap(item));
            }
            #endregion
        }


        private void cbLayoutStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnApplyStyle_Click(object sender, EventArgs e)
        {
            imageGrid1.Column = (int)nudColumn.Value;
            imageGrid1.Gutter = (int)nudGutter.Value;
            imageGrid1.DisplayMode = cbDisplayMode.SelectedIndex == 0 ? ImageGridDisplayMode.StretchImage : ImageGridDisplayMode.ScaleLossCenter;
            imageGrid1.MergeColumn = cbDisplayMode.SelectedIndex == 0 ? true : false;
        }

        private void frmImageGridDemo_Shown(object sender, EventArgs e)
        {
            InitImageGridStyle();
            SetupImageGrid();
        }
    }
}
