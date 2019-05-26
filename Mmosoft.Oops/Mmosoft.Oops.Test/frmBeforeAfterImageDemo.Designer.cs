namespace Mmosoft.Oops.Test
{
    partial class frmBeforeAfterImageDemo
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
            this.beforeAfterImage1 = new Mmosoft.Oops.Controls.BeforeAfterImage();
            this.SuspendLayout();
            // 
            // beforeAfterImage1
            // 
            this.beforeAfterImage1.After = null;
            this.beforeAfterImage1.Before = null;
            this.beforeAfterImage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.beforeAfterImage1.Location = new System.Drawing.Point(0, 0);
            this.beforeAfterImage1.Name = "beforeAfterImage1";
            this.beforeAfterImage1.Size = new System.Drawing.Size(880, 633);
            this.beforeAfterImage1.TabIndex = 0;
            this.beforeAfterImage1.Text = "beforeAfterImage1";
            // 
            // frmBeforeAfterImageDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 633);
            this.Controls.Add(this.beforeAfterImage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBeforeAfterImageDemo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Before After Image Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BeforeAfterImage beforeAfterImage1;
    }
}