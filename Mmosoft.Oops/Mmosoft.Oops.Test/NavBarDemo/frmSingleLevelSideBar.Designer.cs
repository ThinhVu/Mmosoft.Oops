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
            this.pnHeader = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCollapse = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.navBar = new Mmosoft.Oops.SingleLevelNavBar.SingleLevelNavBar();
            this.pnHeader.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // pnHeader
            // 
            this.pnHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnHeader.BackColor = System.Drawing.Color.White;
            this.pnHeader.Controls.Add(this.button7);
            this.pnHeader.Controls.Add(this.button6);
            this.pnHeader.Controls.Add(this.button4);
            this.pnHeader.Location = new System.Drawing.Point(41, 1);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(900, 40);
            this.pnHeader.TabIndex = 5;
            this.pnHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseDown);
            this.pnHeader.MouseEnter += new System.EventHandler(this.pnHeader_MouseEnter);
            this.pnHeader.MouseLeave += new System.EventHandler(this.pnHeader_MouseLeave);
            this.pnHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseMove);
            this.pnHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseUp);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(759, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(47, 40);
            this.button7.TabIndex = 6;
            this.button7.Text = "_";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(806, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(47, 40);
            this.button6.TabIndex = 5;
            this.button6.Text = "[ ]";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(853, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 40);
            this.button4.TabIndex = 4;
            this.button4.Text = "X";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(171, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(770, 513);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "There are some controls  to play with";
            // 
            // btnCollapse
            // 
            this.btnCollapse.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCollapse.Image = null;
            this.btnCollapse.Location = new System.Drawing.Point(1, 1);
            this.btnCollapse.MouseEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(40, 40);
            this.btnCollapse.TabIndex = 35;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // navBar
            // 
            this.navBar.BackColor = System.Drawing.Color.Blue;
            this.navBar.Location = new System.Drawing.Point(1, 41);
            this.navBar.Name = "navBar";
            this.navBar.Size = new System.Drawing.Size(170, 513);
            this.navBar.TabIndex = 36;
            this.navBar.Text = "singleLevelNavBar1";
            // 
            // frmSingleLevelSideBar
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(942, 555);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCollapse);
            this.Controls.Add(this.pnHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(40, 0);
            this.Name = "frmSingleLevelSideBar";
            this.Shown += new System.EventHandler(this.frmSingleLevelSideBar_Shown);
            this.pnHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private Oops.Controls.Buttons.FlatButton btnCollapse;
        private Oops.SingleLevelNavBar.SingleLevelNavBar navBar;
    }
}