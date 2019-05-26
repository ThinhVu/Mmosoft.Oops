namespace Mmosoft.Oops.Test
{
    partial class frmImageGridDemo
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
            this.label3 = new System.Windows.Forms.Label();
            this.nudColumn = new System.Windows.Forms.NumericUpDown();
            this.picSelectedImage = new System.Windows.Forms.PictureBox();
            this.imageGrid1 = new Mmosoft.Oops.Controls.ImageGrid();
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplyStyle
            // 
            this.btnApplyStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyStyle.Location = new System.Drawing.Point(295, 470);
            this.btnApplyStyle.Name = "btnApplyStyle";
            this.btnApplyStyle.Size = new System.Drawing.Size(75, 23);
            this.btnApplyStyle.TabIndex = 30;
            this.btnApplyStyle.Text = "Apply Style";
            this.btnApplyStyle.UseVisualStyleBackColor = true;
            this.btnApplyStyle.Click += new System.EventHandler(this.btnApplyStyle_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 431);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Merge Column: ";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 404);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Display mode: ";
            // 
            // cbMergeColumn
            // 
            this.cbMergeColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMergeColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMergeColumn.FormattingEnabled = true;
            this.cbMergeColumn.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbMergeColumn.Location = new System.Drawing.Point(90, 428);
            this.cbMergeColumn.Name = "cbMergeColumn";
            this.cbMergeColumn.Size = new System.Drawing.Size(127, 21);
            this.cbMergeColumn.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 404);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Gutter:";
            // 
            // cbDisplayMode
            // 
            this.cbDisplayMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplayMode.FormattingEnabled = true;
            this.cbDisplayMode.Items.AddRange(new object[] {
            "Stretch",
            "ScaleLossCenter"});
            this.cbDisplayMode.Location = new System.Drawing.Point(90, 399);
            this.cbDisplayMode.Name = "cbDisplayMode";
            this.cbDisplayMode.Size = new System.Drawing.Size(127, 21);
            this.cbDisplayMode.TabIndex = 26;
            // 
            // nudGutter
            // 
            this.nudGutter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudGutter.Location = new System.Drawing.Point(308, 400);
            this.nudGutter.Name = "nudGutter";
            this.nudGutter.Size = new System.Drawing.Size(62, 20);
            this.nudGutter.TabIndex = 29;
            this.nudGutter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 433);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Row height: ";
            // 
            // nudRowHeight
            // 
            this.nudRowHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudRowHeight.Location = new System.Drawing.Point(308, 431);
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
            this.nudRowHeight.TabIndex = 24;
            this.nudRowHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // cbLayoutStyle
            // 
            this.cbLayoutStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLayoutStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayoutStyle.FormattingEnabled = true;
            this.cbLayoutStyle.Items.AddRange(new object[] {
            "Fill to top",
            "Table"});
            this.cbLayoutStyle.Location = new System.Drawing.Point(90, 370);
            this.cbLayoutStyle.Name = "cbLayoutStyle";
            this.cbLayoutStyle.Size = new System.Drawing.Size(127, 21);
            this.cbLayoutStyle.TabIndex = 25;
            this.cbLayoutStyle.SelectedIndexChanged += new System.EventHandler(this.cbLayoutStyle_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Layout type:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Column: ";
            // 
            // nudColumn
            // 
            this.nudColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudColumn.Location = new System.Drawing.Point(308, 372);
            this.nudColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumn.Name = "nudColumn";
            this.nudColumn.Size = new System.Drawing.Size(62, 20);
            this.nudColumn.TabIndex = 21;
            this.nudColumn.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // picSelectedImage
            // 
            this.picSelectedImage.Location = new System.Drawing.Point(390, 370);
            this.picSelectedImage.Name = "picSelectedImage";
            this.picSelectedImage.Size = new System.Drawing.Size(337, 123);
            this.picSelectedImage.TabIndex = 31;
            this.picSelectedImage.TabStop = false;
            // 
            // imageGrid1
            // 
            this.imageGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageGrid1.LayoutSettings = null;
            this.imageGrid1.Location = new System.Drawing.Point(12, 12);
            this.imageGrid1.Name = "imageGrid1";
            this.imageGrid1.SelectedIndex = 0;
            this.imageGrid1.Size = new System.Drawing.Size(714, 352);
            this.imageGrid1.TabIndex = 17;
            this.imageGrid1.Text = "imageGrid1";
            // 
            // frmImageGridDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 505);
            this.Controls.Add(this.picSelectedImage);
            this.Controls.Add(this.btnApplyStyle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbMergeColumn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbDisplayMode);
            this.Controls.Add(this.nudGutter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudRowHeight);
            this.Controls.Add(this.cbLayoutStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.imageGrid1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudColumn);
            this.Name = "frmImageGridDemo";
            this.Text = "ImageGrid Demo";
            this.Shown += new System.EventHandler(this.frmImageGridDemo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApplyStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbMergeColumn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDisplayMode;
        private System.Windows.Forms.NumericUpDown nudGutter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudRowHeight;
        private System.Windows.Forms.ComboBox cbLayoutStyle;
        private System.Windows.Forms.Label label4;
        private Controls.ImageGrid imageGrid1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudColumn;
        private System.Windows.Forms.PictureBox picSelectedImage;
    }
}