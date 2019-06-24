using System;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    public partial class frmLayerControlDemo : Form
    {
        Mmosoft.Oops.Layers.SketchDrawer skecth;
        Mmosoft.Oops.Layers.BorderDrawer border;

        public frmLayerControlDemo()
        {
            InitializeComponent();

            btnLayered.MouseMove += BtnLayered_MouseMove;
            btnLayered.MouseLeave += BtnLayered_MouseLeave;

            skecth = new Mmosoft.Oops.Layers.SketchDrawer(btnLayered) { StepX = 5, StepY = 5, Mirror = false };
            border = new Mmosoft.Oops.Layers.BorderDrawer(btnLayered);
        }

        private void BtnLayered_MouseLeave(object sender, EventArgs e)
        {
        }

        private void BtnLayered_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void AddLayers()
        {
            btnLayered.PreLayers.Clear();
            btnLayered.PostLayers.Clear();

            if (togBorder.Checked)
                btnLayered.PostLayers.Add(border);
            if (togSketch.Checked)
                btnLayered.PostLayers.Add(skecth);

            btnLayered.Invalidate();
        }

        private void layeredControl1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void togBorder_Click(object sender, EventArgs e)
        {
            AddLayers();            
        }

        private void togBitonal_Click(object sender, EventArgs e)
        {
            AddLayers();
        }

        private void togBlur_Click(object sender, EventArgs e)
        {
            AddLayers();
        }

        private void togSketch_Click(object sender, EventArgs e)
        {
            pnSketch.Visible = togSketch.Checked;

            AddLayers();
        }

        private void txtX_Changed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtX.Text)) return;
            int step = 0;
            if (int.TryParse(txtX.Text, out step))
            {
                skecth.StepX = step;
                btnLayered.Invalidate();
            }
        }

        private void txtY_Changed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtY.Text)) return;
            int step = 0;
            if (int.TryParse(txtY.Text, out step))
            {
                skecth.StepY = step;
                btnLayered.Invalidate();
            }
        }

        private void btnLayered_Click(object sender, EventArgs e)
        {
            using (var fc = new frmControlsDemo())
            {
                fc.ShowDialog();
            }
        }

        private void togRotate_Click(object sender, EventArgs e)
        {
            skecth.Mirror = togRotate.Checked;
            btnLayered.Invalidate();
        }
    }
}
