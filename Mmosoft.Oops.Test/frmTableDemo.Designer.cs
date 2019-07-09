using Mmosoft.Oops.Controls.Buttons;

namespace Mmosoft.OopsTest
{
    partial class frmTableDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleBar1 = new Mmosoft.Oops.Controls.TitleBar.TitleBar();
            this.mediaController1 = new Mmosoft.Oops.Controls.MediaController();
            this.SuspendLayout();
            // 
            // titleBar1
            // 
            this.titleBar1.BackColor = System.Drawing.Color.White;
            this.titleBar1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar1.Location = new System.Drawing.Point(1, 1);
            this.titleBar1.MaximizeEnable = true;
            this.titleBar1.MinimizeEnable = true;
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.Size = new System.Drawing.Size(540, 40);
            this.titleBar1.TabIndex = 1;
            this.titleBar1.Text = "Jukebox";
            // 
            // mediaController1
            // 
            this.mediaController1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaController1.BackColor = System.Drawing.Color.White;
            this.mediaController1.Location = new System.Drawing.Point(1, 530);
            this.mediaController1.Name = "mediaController1";
            this.mediaController1.Size = new System.Drawing.Size(540, 85);
            this.mediaController1.TabIndex = 0;
            // 
            // frmTableDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(542, 616);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.mediaController1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTableDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jukebox";
            this.Load += new System.EventHandler(this.frmTableDemo_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Oops.Controls.MediaController mediaController1;
        private Oops.Controls.TitleBar.TitleBar titleBar1;
    }
}