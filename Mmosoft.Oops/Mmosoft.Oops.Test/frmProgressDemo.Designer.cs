namespace Mmosoft.Oops.Test
{
    partial class frmProgressDemo
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
            this.progressRing1 = new Mmosoft.Oops.Controls.ProgressRing();
            this.progressBar1 = new Mmosoft.Oops.ProgressBar();
            this.progressDots1 = new Mmosoft.Oops.ProgressDots();
            this.SuspendLayout();
            // 
            // progressRing1
            // 
            this.progressRing1.Location = new System.Drawing.Point(75, 46);
            this.progressRing1.Name = "progressRing1";
            this.progressRing1.Size = new System.Drawing.Size(100, 100);
            this.progressRing1.TabIndex = 0;
            this.progressRing1.Text = "progressRing1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(296, 46);
            this.progressBar1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.progressBar1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(299, 12);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Text = "progressBar1";
            this.progressBar1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // progressDots1
            // 
            this.progressDots1.Location = new System.Drawing.Point(296, 113);
            this.progressDots1.Name = "progressDots1";
            this.progressDots1.Size = new System.Drawing.Size(299, 23);
            this.progressDots1.TabIndex = 2;
            this.progressDots1.Text = "progressDots1";
            // 
            // frmProgressDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 267);
            this.Controls.Add(this.progressDots1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.progressRing1);
            this.Name = "frmProgressDemo";
            this.Text = "frmProgressDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ProgressRing progressRing1;
        private ProgressBar progressBar1;
        private ProgressDots progressDots1;
    }
}