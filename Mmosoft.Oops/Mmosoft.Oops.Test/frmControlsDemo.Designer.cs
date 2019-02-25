namespace Mmosoft.OopsTest
{
    partial class frmControlsDemo
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
            Mmosoft.Oops.Colors.ToogleButtonColors toogleButtonColors2 = new Mmosoft.Oops.Colors.ToogleButtonColors();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toogle1 = new Mmosoft.Oops.ToogleButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.marqueeProgressBar1 = new Mmosoft.Oops.Controls.MarqueeProgressBar();
            this.trackBar1 = new Mmosoft.Oops.TrackBar();
            this.progressBar1 = new Mmosoft.Oops.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Dark Background";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.toogle1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(10, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 46);
            this.panel1.TabIndex = 21;
            // 
            // toogle1
            // 
            this.toogle1.Checked = false;
            this.toogle1.Colors = toogleButtonColors2;
            this.toogle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toogle1.Location = new System.Drawing.Point(223, 14);
            this.toogle1.Name = "toogle1";
            this.toogle1.Size = new System.Drawing.Size(46, 19);
            this.toogle1.TabIndex = 13;
            this.toogle1.Text = "toogleButton1";
            this.toogle1.Click += new System.EventHandler(this.toogle1_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.marqueeProgressBar1);
            this.panel6.Controls.Add(this.trackBar1);
            this.panel6.Controls.Add(this.progressBar1);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Location = new System.Drawing.Point(10, 68);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(284, 162);
            this.panel6.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Marquee Progress";
            // 
            // marqueeProgressBar1
            // 
            this.marqueeProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.marqueeProgressBar1.Location = new System.Drawing.Point(18, 25);
            this.marqueeProgressBar1.Name = "marqueeProgressBar1";
            this.marqueeProgressBar1.Size = new System.Drawing.Size(214, 6);
            this.marqueeProgressBar1.TabIndex = 35;
            this.marqueeProgressBar1.Text = "marqueeProgressBar1";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(18, 89);
            this.trackBar1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.trackBar1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(209, 13);
            this.trackBar1.TabIndex = 33;
            this.trackBar1.Text = "trackBar1";
            this.trackBar1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 62);
            this.progressBar1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.progressBar1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(208, 5);
            this.progressBar1.TabIndex = 32;
            this.progressBar1.Text = "progressBar1";
            this.progressBar1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(16, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Range Bar";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(16, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Track Bar";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(15, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Progress Bar";
            // 
            // frmControlsDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(303, 243);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmControlsDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OMmosoft.Oops";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private Mmosoft.Oops.ToogleButton toogle1;
        private Mmosoft.Oops.TrackBar trackBar1;
        private Mmosoft.Oops.ProgressBar progressBar1;
        private Mmosoft.Oops.Controls.MarqueeProgressBar marqueeProgressBar1;
        private System.Windows.Forms.Label label2;
    }
}

