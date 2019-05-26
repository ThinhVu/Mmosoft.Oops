using Mmosoft.Oops.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mmosoft.Oops.Test
{
    public partial class frmImageGridDemo : Form
    {
        public frmImageGridDemo()
        {
            InitializeComponent();
        }

        private void InitImageGridStyle()
        {
            // swich mode to Fill to top
            cbLayoutStyle.SelectedIndex = 0;
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
                imageGrid1.Add(new Bitmap(item));
            #endregion
            imageGrid1.OnItemClicked += (s, e) => { picSelectedImage.Image = e.Image;};
        }


        private void cbLayoutStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLayoutStyle.SelectedIndex == 0)
            {
                // fill to top
                cbDisplayMode.Enabled = false;
                cbMergeColumn.Enabled = false;
                nudRowHeight.Enabled = false;
            }
            else
            {
                // table
                cbDisplayMode.Enabled = true;
                cbMergeColumn.Enabled = true;
                nudRowHeight.Enabled = true;
            }
        }

        private void btnApplyStyle_Click(object sender, EventArgs e)
        {
            if (cbLayoutStyle.SelectedIndex == 0)
            {
                imageGrid1.LayoutSettings = new Mmosoft.Oops.Controls.FillToTop((int)nudColumn.Value, (int)nudGutter.Value);
            }
            else
            {
                imageGrid1.LayoutSettings = new Mmosoft.Oops.Controls.FillToBlock(
                    (int)nudColumn.Value,
                    (int)nudGutter.Value,
                    (int)nudRowHeight.Value,
                    cbMergeColumn.SelectedIndex == 0,
                    cbDisplayMode.SelectedIndex == 0 ? ImageGridDisplayMode.StretchImage : ImageGridDisplayMode.ScaleLossCenter);
            }
        }

        private void frmImageGridDemo_Shown(object sender, EventArgs e)
        {
            InitImageGridStyle();
            SetupImageGrid();
        }
    }
}
