namespace Mmosoft.OopsTest
{
    partial class frmLayerControlDemo
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
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnSketch = new System.Windows.Forms.Panel();
            this.togRotate = new Mmosoft.Oops.ToogleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.togSketch = new Mmosoft.Oops.ToogleButton();
            this.togBlur = new Mmosoft.Oops.ToogleButton();
            this.togBitonal = new Mmosoft.Oops.ToogleButton();
            this.togBorder = new Mmosoft.Oops.ToogleButton();
            this.btnLayered = new Mmosoft.Oops.Layers.LayerControl();
            this.pnSketch.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(52, 30);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(37, 20);
            this.txtX.TabIndex = 1;
            this.txtX.Text = "5";
            this.txtX.TextChanged += new System.EventHandler(this.txtX_Changed);
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(52, 52);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(37, 20);
            this.txtY.TabIndex = 2;
            this.txtY.Text = "5";
            this.txtY.TextChanged += new System.EventHandler(this.txtY_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(29, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(30, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(324, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Show Border";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(306, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Background Blur";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(282, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Background Gray-out";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(351, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Sketch";
            // 
            // pnSketch
            // 
            this.pnSketch.BackColor = System.Drawing.Color.Transparent;
            this.pnSketch.Controls.Add(this.togRotate);
            this.pnSketch.Controls.Add(this.label7);
            this.pnSketch.Controls.Add(this.txtX);
            this.pnSketch.Controls.Add(this.txtY);
            this.pnSketch.Controls.Add(this.label1);
            this.pnSketch.Controls.Add(this.label2);
            this.pnSketch.Location = new System.Drawing.Point(342, 104);
            this.pnSketch.Name = "pnSketch";
            this.pnSketch.Size = new System.Drawing.Size(93, 75);
            this.pnSketch.TabIndex = 13;
            this.pnSketch.Visible = false;
            // 
            // togRotate
            // 
            this.togRotate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(221)))));
            this.togRotate.Checked = false;
            this.togRotate.Location = new System.Drawing.Point(52, 7);
            this.togRotate.Name = "togRotate";
            this.togRotate.Size = new System.Drawing.Size(41, 17);
            this.togRotate.TabIndex = 14;
            this.togRotate.Text = "toogleButton4";
            this.togRotate.Click += new System.EventHandler(this.togRotate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(11, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Rotate";
            // 
            // togSketch
            // 
            this.togSketch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(221)))));
            this.togSketch.Checked = false;
            this.togSketch.Location = new System.Drawing.Point(396, 81);
            this.togSketch.Name = "togSketch";
            this.togSketch.Size = new System.Drawing.Size(39, 17);
            this.togSketch.TabIndex = 12;
            this.togSketch.Text = "toogleButton4";
            this.togSketch.Click += new System.EventHandler(this.togSketch_Click);
            // 
            // togBlur
            // 
            this.togBlur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(221)))));
            this.togBlur.Checked = false;
            this.togBlur.Location = new System.Drawing.Point(396, 35);
            this.togBlur.Name = "togBlur";
            this.togBlur.Size = new System.Drawing.Size(39, 17);
            this.togBlur.TabIndex = 10;
            this.togBlur.Text = "toogleButton3";
            this.togBlur.Click += new System.EventHandler(this.togBlur_Click);
            // 
            // togBitonal
            // 
            this.togBitonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(221)))));
            this.togBitonal.Checked = false;
            this.togBitonal.Location = new System.Drawing.Point(396, 11);
            this.togBitonal.Name = "togBitonal";
            this.togBitonal.Size = new System.Drawing.Size(39, 17);
            this.togBitonal.TabIndex = 6;
            this.togBitonal.Text = "toogleButton2";
            this.togBitonal.Click += new System.EventHandler(this.togBitonal_Click);
            // 
            // togBorder
            // 
            this.togBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(221)))));
            this.togBorder.Checked = false;
            this.togBorder.Location = new System.Drawing.Point(396, 58);
            this.togBorder.Name = "togBorder";
            this.togBorder.Size = new System.Drawing.Size(39, 17);
            this.togBorder.TabIndex = 5;
            this.togBorder.Text = "toogleButton1";
            this.togBorder.Click += new System.EventHandler(this.togBorder_Click);
            // 
            // btnLayered
            // 
            this.btnLayered.BackColor = System.Drawing.Color.Transparent;
            this.btnLayered.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayered.ForeColor = System.Drawing.Color.DeepPink;
            this.btnLayered.Location = new System.Drawing.Point(201, 220);
            this.btnLayered.Name = "btnLayered";
            this.btnLayered.Size = new System.Drawing.Size(226, 71);
            this.btnLayered.TabIndex = 0;
            this.btnLayered.Text = "♥ Hit me!";
            this.btnLayered.Click += new System.EventHandler(this.btnLayered_Click);
            // 
            // frmHitMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(439, 336);
            this.Controls.Add(this.pnSketch);
            this.Controls.Add(this.togSketch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.togBlur);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.togBitonal);
            this.Controls.Add(this.togBorder);
            this.Controls.Add(this.btnLayered);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmHitMe";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hit Me!";
            this.pnSketch.ResumeLayout(false);
            this.pnSketch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mmosoft.Oops.Layers.LayerControl btnLayered;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Mmosoft.Oops.ToogleButton togBorder;
        private Mmosoft.Oops.ToogleButton togBitonal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Mmosoft.Oops.ToogleButton togBlur;
        private Mmosoft.Oops.ToogleButton togSketch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnSketch;
        private Mmosoft.Oops.ToogleButton togRotate;
        private System.Windows.Forms.Label label7;        
    }
}