namespace Mmosoft.Oops.Test
{
    partial class frmImageSlide
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
            this.imageSlide1 = new Mmosoft.Oops.Controls.Images.ImageSlide();
            this.SuspendLayout();
            // 
            // imageSlide1
            // 
            this.imageSlide1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.imageSlide1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSlide1.Location = new System.Drawing.Point(0, 0);
            this.imageSlide1.Name = "imageSlide1";
            this.imageSlide1.Size = new System.Drawing.Size(559, 338);
            this.imageSlide1.TabIndex = 0;
            this.imageSlide1.Text = "imageSlide1";
            // 
            // frmImageSlide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 338);
            this.Controls.Add(this.imageSlide1);
            this.Name = "frmImageSlide";
            this.Text = "frmImageSlide";
            this.Load += new System.EventHandler(this.frmImageSlide_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Images.ImageSlide imageSlide1;
    }
}