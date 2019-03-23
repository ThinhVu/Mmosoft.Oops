namespace Mmosoft.OopsTest.SideBarDemo
{
    partial class frmSingleLevelSideBar
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
            this.label1 = new System.Windows.Forms.Label();
            this.pnContent = new System.Windows.Forms.Panel();
            this.mediaController1 = new Mmosoft.Oops.Controls.MediaController();
            this.imageGrid1 = new Mmosoft.Oops.Controls.ImageGrid();
            this.navBar = new Mmosoft.Oops.SingleLevelNavBar.SingleLevelNavBar();
            this.titleBar1 = new Mmosoft.Oops.Controls.TitleBar.TitleBar();
            this.btnCollapse = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.pnContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Section";
            // 
            // pnContent
            // 
            this.pnContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnContent.BackColor = System.Drawing.Color.White;
            this.pnContent.Controls.Add(this.mediaController1);
            this.pnContent.Controls.Add(this.imageGrid1);
            this.pnContent.Controls.Add(this.label1);
            this.pnContent.Location = new System.Drawing.Point(235, 41);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(873, 513);
            this.pnContent.TabIndex = 6;
            // 
            // mediaController1
            // 
            this.mediaController1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mediaController1.BackColor = System.Drawing.Color.White;
            this.mediaController1.Location = new System.Drawing.Point(6, 424);
            this.mediaController1.Name = "mediaController1";
            this.mediaController1.Size = new System.Drawing.Size(298, 73);
            this.mediaController1.TabIndex = 5;
            // 
            // imageGrid1
            // 
            this.imageGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageGrid1.Column = 3;
            this.imageGrid1.ImagePadding = 5;
            this.imageGrid1.Location = new System.Drawing.Point(13, 41);
            this.imageGrid1.Name = "imageGrid1";
            this.imageGrid1.Size = new System.Drawing.Size(849, 377);
            this.imageGrid1.TabIndex = 4;
            this.imageGrid1.Text = "imageGrid1";
            // 
            // navBar
            // 
            this.navBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.navBar.BackColor = System.Drawing.Color.Gainsboro;
            this.navBar.ClickedItem = System.Drawing.Color.Blue;
            this.navBar.Location = new System.Drawing.Point(1, 1);
            this.navBar.Name = "navBar";
            this.navBar.Size = new System.Drawing.Size(234, 553);
            this.navBar.TabIndex = 37;
            this.navBar.Text = "singleLevelNavBar1";
            // 
            // titleBar1
            // 
            this.titleBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBar1.BackColor = System.Drawing.Color.White;
            this.titleBar1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar1.Location = new System.Drawing.Point(235, 1);
            this.titleBar1.MaximizeEnable = true;
            this.titleBar1.MinimizeEnable = true;
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.Size = new System.Drawing.Size(873, 40);
            this.titleBar1.TabIndex = 36;
            this.titleBar1.Text = "Demo sample";
            // 
            // btnCollapse
            // 
            this.btnCollapse.Image = null;
            this.btnCollapse.Location = new System.Drawing.Point(13, 13);
            this.btnCollapse.MouseEnterColor = System.Drawing.Color.Empty;
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(75, 23);
            this.btnCollapse.TabIndex = 8;
            this.btnCollapse.Text = "flatButton1";
            // 
            // frmSingleLevelSideBar
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1109, 555);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.btnCollapse);
            this.Controls.Add(this.pnContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(40, 0);
            this.Name = "frmSingleLevelSideBar";
            this.Shown += new System.EventHandler(this.frmSingleLevelSideBar_Shown);
            this.pnContent.ResumeLayout(false);
            this.pnContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnContent;
        private Oops.Controls.Buttons.FlatButton btnCollapse;
        private Oops.Controls.TitleBar.TitleBar titleBar1;
        private Oops.SingleLevelNavBar.SingleLevelNavBar navBar;
        private Oops.Controls.ImageGrid imageGrid1;
        private Oops.Controls.MediaController mediaController1;
    }
}