namespace Mmosoft.OopsTest.SideBarDemo
{
    partial class frmMultiLevelSideBar
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.sideBar1 = new Mmosoft.Oops.MultiLevelNavBar();
            this.titleBar1 = new Mmosoft.Oops.Controls.TitleBar.TitleBar();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(253, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(779, 514);
            this.panel4.TabIndex = 2;
            // 
            // sideBar1
            // 
            this.sideBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sideBar1.BackColor = System.Drawing.Color.Gainsboro;
            this.sideBar1.EnableHighlightReveal = false;
            this.sideBar1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sideBar1.Location = new System.Drawing.Point(1, 1);
            this.sideBar1.Name = "sideBar1";
            this.sideBar1.Size = new System.Drawing.Size(253, 554);
            this.sideBar1.TabIndex = 0;
            this.sideBar1.Text = "sideBar1";
            // 
            // titleBar1
            // 
            this.titleBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBar1.BackColor = System.Drawing.Color.White;
            this.titleBar1.Location = new System.Drawing.Point(253, 1);
            this.titleBar1.MaximizeEnable = true;
            this.titleBar1.MinimizeEnable = true;
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.Size = new System.Drawing.Size(779, 40);
            this.titleBar1.TabIndex = 3;
            this.titleBar1.Text = "titleBar1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // frmMultiLevelSideBar
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1033, 556);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.sideBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMultiLevelSideBar";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Mmosoft.Oops.MultiLevelNavBar sideBar1;
        private System.Windows.Forms.Panel panel4;
        private Oops.Controls.TitleBar.TitleBar titleBar1;
        private System.Windows.Forms.Label label1;
    }
}