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
            this.SuspendLayout();
            // 
            // progressRing1
            // 
            this.progressRing1.Location = new System.Drawing.Point(75, 46);
            this.progressRing1.Name = "progressRing1";
            this.progressRing1.Size = new System.Drawing.Size(177, 136);
            this.progressRing1.TabIndex = 0;
            this.progressRing1.Text = "progressRing1";
            // 
            // frmProgressDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.progressRing1);
            this.Name = "frmProgressDemo";
            this.Text = "frmProgressDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ProgressRing progressRing1;
    }
}