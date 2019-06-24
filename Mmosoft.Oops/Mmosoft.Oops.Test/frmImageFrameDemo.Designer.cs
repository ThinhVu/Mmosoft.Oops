namespace Mmosoft.Oops.Test
{
    partial class frmImageFrameDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImageFrameDemo));
            this.imageFrame1 = new Mmosoft.Oops.Controls.Images.ImageFrame();
            this.SuspendLayout();
            // 
            // imageFrame1
            // 
            this.imageFrame1.BackColor = System.Drawing.Color.Transparent;
            this.imageFrame1.FrameSize = new System.Drawing.Size(100, 150);
            this.imageFrame1.Location = new System.Drawing.Point(186, 362);
            this.imageFrame1.LoopEnable = false;
            this.imageFrame1.Name = "imageFrame1";
            this.imageFrame1.Run = false;
            this.imageFrame1.Size = new System.Drawing.Size(40, 60);
            this.imageFrame1.Source = ((System.Drawing.Image)(resources.GetObject("imageFrame1.Source")));
            this.imageFrame1.TabIndex = 0;
            this.imageFrame1.Text = "imageFrame1";
            // 
            // frmImageFrameDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(735, 448);
            this.Controls.Add(this.imageFrame1);
            this.Name = "frmImageFrameDemo";
            this.Text = "frmImageFrameDemo";
            this.Load += new System.EventHandler(this.frmImageFrameDemo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmImageFrameDemo_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmImageFrameDemo_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Images.ImageFrame imageFrame1;

    }
}