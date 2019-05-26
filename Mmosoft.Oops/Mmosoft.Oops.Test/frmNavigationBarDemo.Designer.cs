namespace Mmosoft.OopsTest
{
    partial class frmNavigationBarDemo
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
            Mmosoft.Oops.Colors.ToogleButtonColors toogleButtonColors5 = new Mmosoft.Oops.Colors.ToogleButtonColors();
            Mmosoft.Oops.Colors.ToogleButtonColors toogleButtonColors6 = new Mmosoft.Oops.Colors.ToogleButtonColors();
            this.label1 = new System.Windows.Forms.Label();
            this.pnContent = new System.Windows.Forms.Panel();
            this.nudItemHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudIconSize = new System.Windows.Forms.NumericUpDown();
            this.nudIdentWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudDropdownSize = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.navBar = new Mmosoft.Oops.Controls.NavigationBar();
            this.btnApplyStyle = new Mmosoft.Oops.Controls.Buttons.Button();
            this.togHighlightRevealEnabled = new Mmosoft.Oops.ToogleButton();
            this.togCollapseExpandEnable = new Mmosoft.Oops.ToogleButton();
            this.pnContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdentWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropdownSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label1.Location = new System.Drawing.Point(7, 9);
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
            this.pnContent.Controls.Add(this.nudDropdownSize);
            this.pnContent.Controls.Add(this.label7);
            this.pnContent.Controls.Add(this.btnApplyStyle);
            this.pnContent.Controls.Add(this.label6);
            this.pnContent.Controls.Add(this.togHighlightRevealEnabled);
            this.pnContent.Controls.Add(this.label5);
            this.pnContent.Controls.Add(this.togCollapseExpandEnable);
            this.pnContent.Controls.Add(this.nudIdentWidth);
            this.pnContent.Controls.Add(this.label4);
            this.pnContent.Controls.Add(this.nudIconSize);
            this.pnContent.Controls.Add(this.label3);
            this.pnContent.Controls.Add(this.label2);
            this.pnContent.Controls.Add(this.nudItemHeight);
            this.pnContent.Controls.Add(this.label1);
            this.pnContent.Location = new System.Drawing.Point(236, 0);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(254, 424);
            this.pnContent.TabIndex = 6;
            // 
            // nudItemHeight
            // 
            this.nudItemHeight.Location = new System.Drawing.Point(163, 63);
            this.nudItemHeight.Name = "nudItemHeight";
            this.nudItemHeight.Size = new System.Drawing.Size(46, 20);
            this.nudItemHeight.TabIndex = 4;
            this.nudItemHeight.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Item Height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Item Icon size:";
            // 
            // nudIconSize
            // 
            this.nudIconSize.Location = new System.Drawing.Point(163, 89);
            this.nudIconSize.Name = "nudIconSize";
            this.nudIconSize.Size = new System.Drawing.Size(46, 20);
            this.nudIconSize.TabIndex = 7;
            this.nudIconSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nudIdentWidth
            // 
            this.nudIdentWidth.Location = new System.Drawing.Point(163, 115);
            this.nudIdentWidth.Name = "nudIdentWidth";
            this.nudIdentWidth.Size = new System.Drawing.Size(46, 20);
            this.nudIdentWidth.TabIndex = 9;
            this.nudIdentWidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ident width: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Collapse/Expand Enabled: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Highlight Reveal Enabled: ";
            // 
            // nudDropdownSize
            // 
            this.nudDropdownSize.Location = new System.Drawing.Point(163, 141);
            this.nudDropdownSize.Name = "nudDropdownSize";
            this.nudDropdownSize.Size = new System.Drawing.Size(46, 20);
            this.nudDropdownSize.TabIndex = 16;
            this.nudDropdownSize.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Dropdown size: ";
            // 
            // navBar
            // 
            this.navBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.navBar.BackColor = System.Drawing.Color.Gainsboro;
            this.navBar.CollapseExpandEnable = false;
            this.navBar.DropDownSize = 6;
            this.navBar.EnableHighlightReveal = false;
            this.navBar.IdentWidth = 20;
            this.navBar.ItemHeight = 40;
            this.navBar.ItemIconSize = 16;
            this.navBar.Location = new System.Drawing.Point(0, 0);
            this.navBar.Name = "navBar";
            this.navBar.Size = new System.Drawing.Size(237, 424);
            this.navBar.TabIndex = 37;
            this.navBar.Text = "singleLevelNavBar1";
            // 
            // btnApplyStyle
            // 
            this.btnApplyStyle.Location = new System.Drawing.Point(134, 239);
            this.btnApplyStyle.Name = "btnApplyStyle";
            this.btnApplyStyle.Size = new System.Drawing.Size(75, 23);
            this.btnApplyStyle.TabIndex = 14;
            this.btnApplyStyle.Text = "Apply";
            this.btnApplyStyle.Click += new System.EventHandler(this.btnApplyStyle_Click);
            // 
            // togHighlightRevealEnabled
            // 
            this.togHighlightRevealEnabled.Checked = false;
            this.togHighlightRevealEnabled.Colors = toogleButtonColors5;
            this.togHighlightRevealEnabled.Location = new System.Drawing.Point(163, 200);
            this.togHighlightRevealEnabled.Name = "togHighlightRevealEnabled";
            this.togHighlightRevealEnabled.Size = new System.Drawing.Size(46, 23);
            this.togHighlightRevealEnabled.TabIndex = 12;
            this.togHighlightRevealEnabled.Text = "toogleButton2";
            // 
            // togCollapseExpandEnable
            // 
            this.togCollapseExpandEnable.Checked = false;
            this.togCollapseExpandEnable.Colors = toogleButtonColors6;
            this.togCollapseExpandEnable.Location = new System.Drawing.Point(163, 171);
            this.togCollapseExpandEnable.Name = "togCollapseExpandEnable";
            this.togCollapseExpandEnable.Size = new System.Drawing.Size(46, 23);
            this.togCollapseExpandEnable.TabIndex = 10;
            this.togCollapseExpandEnable.Text = "toogleButton1";
            // 
            // frmNavigationBarDemo
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(490, 424);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.pnContent);
            this.Location = new System.Drawing.Point(40, 0);
            this.Name = "frmNavigationBarDemo";
            this.Text = "Navigation bar demo";
            this.Shown += new System.EventHandler(this.frmSingleLevelSideBar_Shown);
            this.pnContent.ResumeLayout(false);
            this.pnContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdentWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropdownSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnContent;
        private Oops.Controls.NavigationBar navBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudItemHeight;
        private System.Windows.Forms.NumericUpDown nudIconSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudIdentWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Oops.ToogleButton togCollapseExpandEnable;
        private System.Windows.Forms.Label label6;
        private Oops.ToogleButton togHighlightRevealEnabled;
        private Oops.Controls.Buttons.Button btnApplyStyle;
        private System.Windows.Forms.NumericUpDown nudDropdownSize;
        private System.Windows.Forms.Label label7;
    }
}