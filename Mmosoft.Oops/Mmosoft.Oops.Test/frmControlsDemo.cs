using Mmosoft.Oops;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    public partial class frmControlsDemo : Form
    {
        Mmosoft.Oops.Animation.Animator anim;
        public frmControlsDemo()
        {
            InitializeComponent();

            anim = new Mmosoft.Oops.Animation.Animator();
            anim.Add(new Mmosoft.Oops.Animation.Step
            {
                Interval = 30,
                TotalStep = 100,
                AnimAction = (i) => progressBar1.Value = i
            });
            anim.Loop = true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ConfigBar();
            anim.Start();
            marqueeProgressBar1.Start();
        }

        private void ConfigBar()
        {
            trackBar1.Value = 20;
        }
      
        private void toogle1_Click(object sender, EventArgs e)
        {
            if (toogle1.Checked)
                this.BackColor = CustomColorTranslator.Get("70, 70, 70");
            else
                this.BackColor = Color.White;
        }
    }
}
