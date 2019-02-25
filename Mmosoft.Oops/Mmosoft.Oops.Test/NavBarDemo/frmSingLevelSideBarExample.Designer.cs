namespace Mmosoft.OopsTest.SideBarDemo
{
    partial class frmSideBarExample
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
            this.sideBar2 = new Mmosoft.Oops.NavBar();
            this.SuspendLayout();
            // 
            // sideBar2
            // 
            this.sideBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sideBar2.Location = new System.Drawing.Point(0, 0);
            this.sideBar2.Name = "sideBar2";
            this.sideBar2.Size = new System.Drawing.Size(202, 450);
            this.sideBar2.TabIndex = 0;
            this.sideBar2.Text = "sideBar1";
            // 
            // frmSideBarExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sideBar2);
            this.Name = "frmSideBarExample";
            this.Text = "frmSideBarExample";
            this.Load += new System.EventHandler(this.frmSideBarExample_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Mmosoft.Oops.NavBar sideBar2;
    }
}