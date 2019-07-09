namespace Mmosoft.Oops.Test
{
    partial class frmStackImageGridDemo
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
            this.label7 = new System.Windows.Forms.Label();
            this.nudGutter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudColumn = new System.Windows.Forms.NumericUpDown();
            this.imageGrid1 = new Mmosoft.Oops.Controls.StackImageGrid();
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplyStyle
            // 
            this.btnApplyStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyStyle.Location = new System.Drawing.Point(262, 376);
            this.btnApplyStyle.Name = "btnApplyStyle";
            this.btnApplyStyle.Size = new System.Drawing.Size(75, 23);
            this.btnApplyStyle.TabIndex = 30;
            this.btnApplyStyle.Text = "Apply Style";
            this.btnApplyStyle.UseVisualStyleBackColor = true;
            this.btnApplyStyle.Click += new System.EventHandler(this.btnApplyStyle_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 383);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Gutter:";
            // 
            // nudGutter
            // 
            this.nudGutter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudGutter.Location = new System.Drawing.Point(194, 379);
            this.nudGutter.Name = "nudGutter";
            this.nudGutter.Size = new System.Drawing.Size(62, 20);
            this.nudGutter.TabIndex = 29;
            this.nudGutter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 379);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Column: ";
            // 
            // nudColumn
            // 
            this.nudColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudColumn.Location = new System.Drawing.Point(66, 377);
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
            // imageGrid1
            // 
            this.imageGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageGrid1.Column = 3;
            this.imageGrid1.Gutter = 0;
            this.imageGrid1.Location = new System.Drawing.Point(12, 12);
            this.imageGrid1.Name = "imageGrid1";
            this.imageGrid1.Size = new System.Drawing.Size(714, 358);
            this.imageGrid1.TabIndex = 31;
            this.imageGrid1.Text = "imageGrid1";
            // 
            // frmStackImageGridDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 411);
            this.Controls.Add(this.btnApplyStyle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudGutter);
            this.Controls.Add(this.imageGrid1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudColumn);
            this.Name = "frmStackImageGridDemo";
            this.Text = "ImageGrid Demo";
            this.Shown += new System.EventHandler(this.frmImageGridDemo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApplyStyle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudGutter;
        private Controls.StackImageGrid imageGrid1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudColumn;
    }
}