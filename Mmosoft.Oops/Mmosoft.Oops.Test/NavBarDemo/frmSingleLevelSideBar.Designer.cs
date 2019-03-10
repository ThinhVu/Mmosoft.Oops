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
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new Mmosoft.Oops.Controls.Buttons.Button();
            this.button5 = new Mmosoft.Oops.Controls.Buttons.Button();
            this.btnSvgPath = new Mmosoft.Oops.Controls.Buttons.Button();
            this.btnTable = new Mmosoft.Oops.Controls.Buttons.Button();
            this.btnBoxShadow = new Mmosoft.Oops.Controls.Buttons.Button();
            this.button3 = new Mmosoft.Oops.Controls.Buttons.Button();
            this.button2 = new Mmosoft.Oops.Controls.Buttons.Button();
            this.navBar = new Mmosoft.Oops.SingleLevelNavBar.NavBar();
            this.pnHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Section";
            // 
            // pnHeader
            // 
            this.pnHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnHeader.BackColor = System.Drawing.Color.White;
            this.pnHeader.Controls.Add(this.button4);
            this.pnHeader.Controls.Add(this.label1);
            this.pnHeader.Location = new System.Drawing.Point(252, 1);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(493, 42);
            this.pnHeader.TabIndex = 5;
            this.pnHeader.DoubleClick += new System.EventHandler(this.pnHeader_DoubleClick);
            this.pnHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseDown);
            this.pnHeader.MouseEnter += new System.EventHandler(this.pnHeader_MouseEnter);
            this.pnHeader.MouseLeave += new System.EventHandler(this.pnHeader_MouseLeave);
            this.pnHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseMove);
            this.pnHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseUp);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(435, 8);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Close";
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
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.btnSvgPath);
            this.panel2.Controls.Add(this.btnTable);
            this.panel2.Controls.Add(this.btnBoxShadow);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(252, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(493, 391);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "There are some controls  to play with";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button1.Location = new System.Drawing.Point(13, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 32);
            this.button1.TabIndex = 33;
            this.button1.Text = "Disabled button";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(14, 40);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(215, 32);
            this.button5.TabIndex = 32;
            this.button5.Text = "Multi-level Navbar";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnSvgPath
            // 
            this.btnSvgPath.BackColor = System.Drawing.Color.Transparent;
            this.btnSvgPath.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSvgPath.Location = new System.Drawing.Point(13, 116);
            this.btnSvgPath.Name = "btnSvgPath";
            this.btnSvgPath.Size = new System.Drawing.Size(216, 32);
            this.btnSvgPath.TabIndex = 31;
            this.btnSvgPath.Text = "Svg Path Image creator";
            this.btnSvgPath.Click += new System.EventHandler(this.btnSvgPath_Click);
            // 
            // btnTable
            // 
            this.btnTable.BackColor = System.Drawing.Color.Transparent;
            this.btnTable.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnTable.Location = new System.Drawing.Point(235, 78);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(216, 32);
            this.btnTable.TabIndex = 30;
            this.btnTable.Text = "Table";
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnBoxShadow
            // 
            this.btnBoxShadow.BackColor = System.Drawing.Color.Transparent;
            this.btnBoxShadow.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnBoxShadow.Location = new System.Drawing.Point(235, 116);
            this.btnBoxShadow.Name = "btnBoxShadow";
            this.btnBoxShadow.Size = new System.Drawing.Size(216, 32);
            this.btnBoxShadow.TabIndex = 28;
            this.btnBoxShadow.Text = "Box Shadow Control";
            this.btnBoxShadow.Click += new System.EventHandler(this.btnBoxShadow_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button3.Location = new System.Drawing.Point(235, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(216, 32);
            this.button3.TabIndex = 27;
            this.button3.Text = "Controls";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button2.Location = new System.Drawing.Point(14, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(215, 32);
            this.button2.TabIndex = 26;
            this.button2.Text = "Layer control";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // navBar
            // 
            this.navBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.navBar.BackColor = System.Drawing.Color.GreenYellow;
            this.navBar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBar.Location = new System.Drawing.Point(1, 1);
            this.navBar.Name = "navBar";
            this.navBar.Size = new System.Drawing.Size(252, 433);
            this.navBar.TabIndex = 0;
            this.navBar.Text = "sideBar1";
            // 
            // frmSingleLevelSideBar
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(746, 435);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnHeader);
            this.Controls.Add(this.navBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSingleLevelSideBar";
            this.Shown += new System.EventHandler(this.frmSingleLevelSideBar_Shown);
            this.LocationChanged += new System.EventHandler(this.frmSingleLevelSideBar_LocationChanged);
            this.pnHeader.ResumeLayout(false);
            this.pnHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Mmosoft.Oops.SingleLevelNavBar.NavBar navBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private Oops.Controls.Buttons.Button button1;
        private Oops.Controls.Buttons.Button button5;
        private Oops.Controls.Buttons.Button btnSvgPath;
        private Oops.Controls.Buttons.Button btnTable;
        private Oops.Controls.Buttons.Button btnBoxShadow;
        private Oops.Controls.Buttons.Button button3;
        private Oops.Controls.Buttons.Button button2;
        private System.Windows.Forms.Label label2;
    }
}