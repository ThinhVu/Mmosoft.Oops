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
            this.label1 = new System.Windows.Forms.Label();
            this.pnContent = new System.Windows.Forms.Panel();
            this.btnApplyStyle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbMergeColumn = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDisplayMode = new System.Windows.Forms.ComboBox();
            this.nudGutter = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudRowHeight = new System.Windows.Forms.NumericUpDown();
            this.cbLayoutStyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.imageGrid1 = new Mmosoft.Oops.Controls.ImageGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.nudColumn = new System.Windows.Forms.NumericUpDown();
            this.navBar = new Mmosoft.Oops.Controls.NavigationBar();
            this.titleBar1 = new Mmosoft.Oops.Controls.TitleBar.TitleBar();
            this.btnCollapse = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.pnContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumn)).BeginInit();
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
            this.pnContent.Controls.Add(this.btnApplyStyle);
            this.pnContent.Controls.Add(this.label2);
            this.pnContent.Controls.Add(this.label6);
            this.pnContent.Controls.Add(this.cbMergeColumn);
            this.pnContent.Controls.Add(this.label7);
            this.pnContent.Controls.Add(this.cbDisplayMode);
            this.pnContent.Controls.Add(this.nudGutter);
            this.pnContent.Controls.Add(this.label5);
            this.pnContent.Controls.Add(this.nudRowHeight);
            this.pnContent.Controls.Add(this.cbLayoutStyle);
            this.pnContent.Controls.Add(this.label4);
            this.pnContent.Controls.Add(this.imageGrid1);
            this.pnContent.Controls.Add(this.label3);
            this.pnContent.Controls.Add(this.label1);
            this.pnContent.Controls.Add(this.nudColumn);
            this.pnContent.Location = new System.Drawing.Point(235, 41);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(873, 513);
            this.pnContent.TabIndex = 6;
            // 
            // btnApplyStyle
            // 
            this.btnApplyStyle.Location = new System.Drawing.Point(409, 415);
            this.btnApplyStyle.Name = "btnApplyStyle";
            this.btnApplyStyle.Size = new System.Drawing.Size(75, 23);
            this.btnApplyStyle.TabIndex = 16;
            this.btnApplyStyle.Text = "Apply Style";
            this.btnApplyStyle.UseVisualStyleBackColor = true;
            this.btnApplyStyle.Click += new System.EventHandler(this.btnApplyStyle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 477);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Merge Column: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 450);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Display mode: ";
            // 
            // cbMergeColumn
            // 
            this.cbMergeColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMergeColumn.FormattingEnabled = true;
            this.cbMergeColumn.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbMergeColumn.Location = new System.Drawing.Point(91, 474);
            this.cbMergeColumn.Name = "cbMergeColumn";
            this.cbMergeColumn.Size = new System.Drawing.Size(127, 21);
            this.cbMergeColumn.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(264, 450);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Gutter:";
            // 
            // cbDisplayMode
            // 
            this.cbDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplayMode.FormattingEnabled = true;
            this.cbDisplayMode.Items.AddRange(new object[] {
            "Stretch",
            "ScaleLossCenter"});
            this.cbDisplayMode.Location = new System.Drawing.Point(91, 445);
            this.cbDisplayMode.Name = "cbDisplayMode";
            this.cbDisplayMode.Size = new System.Drawing.Size(127, 21);
            this.cbDisplayMode.TabIndex = 12;
            // 
            // nudGutter
            // 
            this.nudGutter.Location = new System.Drawing.Point(309, 446);
            this.nudGutter.Name = "nudGutter";
            this.nudGutter.Size = new System.Drawing.Size(62, 20);
            this.nudGutter.TabIndex = 14;
            this.nudGutter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 479);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Row height: ";
            // 
            // nudRowHeight
            // 
            this.nudRowHeight.Location = new System.Drawing.Point(309, 477);
            this.nudRowHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudRowHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRowHeight.Name = "nudRowHeight";
            this.nudRowHeight.Size = new System.Drawing.Size(62, 20);
            this.nudRowHeight.TabIndex = 11;
            this.nudRowHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // cbLayoutStyle
            // 
            this.cbLayoutStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayoutStyle.FormattingEnabled = true;
            this.cbLayoutStyle.Items.AddRange(new object[] {
            "Fill to top",
            "Table"});
            this.cbLayoutStyle.Location = new System.Drawing.Point(91, 416);
            this.cbLayoutStyle.Name = "cbLayoutStyle";
            this.cbLayoutStyle.Size = new System.Drawing.Size(127, 21);
            this.cbLayoutStyle.TabIndex = 11;
            this.cbLayoutStyle.SelectedIndexChanged += new System.EventHandler(this.cbLayoutStyle_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 418);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Layout type:";
            // 
            // imageGrid1
            // 
            this.imageGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageGrid1.GridLayout = null;
            this.imageGrid1.Location = new System.Drawing.Point(13, 41);
            this.imageGrid1.Name = "imageGrid1";
            this.imageGrid1.SelectedIndex = 0;
            this.imageGrid1.Size = new System.Drawing.Size(849, 352);
            this.imageGrid1.TabIndex = 4;
            this.imageGrid1.Text = "imageGrid1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Column: ";
            // 
            // nudColumn
            // 
            this.nudColumn.Location = new System.Drawing.Point(309, 418);
            this.nudColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumn.Name = "nudColumn";
            this.nudColumn.Size = new System.Drawing.Size(62, 20);
            this.nudColumn.TabIndex = 9;
            this.nudColumn.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // navBar
            // 
            this.navBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.navBar.BackColor = System.Drawing.Color.Gainsboro;
            this.navBar.EnableHighlightReveal = false;
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
            // frmNavigationBarDemo
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1109, 555);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.btnCollapse);
            this.Controls.Add(this.pnContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(40, 0);
            this.Name = "frmNavigationBarDemo";
            this.Shown += new System.EventHandler(this.frmSingleLevelSideBar_Shown);
            this.pnContent.ResumeLayout(false);
            this.pnContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnContent;
        private Oops.Controls.Buttons.FlatButton btnCollapse;
        private Oops.Controls.TitleBar.TitleBar titleBar1;
        private Oops.Controls.NavigationBar navBar;
        private Oops.Controls.ImageGrid imageGrid1;
        private System.Windows.Forms.ComboBox cbMergeColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLayoutStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudRowHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDisplayMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudGutter;
        private System.Windows.Forms.Button btnApplyStyle;
    }
}