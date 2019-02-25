using Mmosoft.Oops.Controls.Buttons;

namespace Mmosoft.OopsTest
{
    partial class frmPortal
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
            Mmosoft.Oops.Controls.Lines.LineColors lineColors1 = new Mmosoft.Oops.Controls.Lines.LineColors();
            this.button5 = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.horizontalLine1 = new Mmosoft.Oops.Controls.Lines.HorizontalLine();
            this.btnSvgPath = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.btnTable = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.navBarSingle = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.btnBoxShadow = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.button3 = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.button2 = new Mmosoft.Oops.Controls.Buttons.BorderRadiusButton();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.button5.Location = new System.Drawing.Point(12, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 25);
            this.button5.TabIndex = 16;
            this.button5.Text = "Navbar multi-level";
            this.button5.Click += new System.EventHandler(this.btnNavBarMulti_Click);
            // 
            // horizontalLine1
            // 
            lineColors1.LineColor = "#0";
            this.horizontalLine1.LineColors = lineColors1;
            this.horizontalLine1.LineWeight = 1;
            this.horizontalLine1.Location = new System.Drawing.Point(15, 98);
            this.horizontalLine1.Name = "horizontalLine1";
            this.horizontalLine1.Size = new System.Drawing.Size(463, 10);
            this.horizontalLine1.TabIndex = 14;
            this.horizontalLine1.Text = "horizontalLine1";
            // 
            // btnSvgPath
            // 
            this.btnSvgPath.BackColor = System.Drawing.Color.Transparent;
            this.btnSvgPath.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.btnSvgPath.Location = new System.Drawing.Point(12, 64);
            this.btnSvgPath.Name = "btnSvgPath";
            this.btnSvgPath.Size = new System.Drawing.Size(150, 25);
            this.btnSvgPath.TabIndex = 13;
            this.btnSvgPath.Text = "Svg Path Image creator";
            this.btnSvgPath.Click += new System.EventHandler(this.btnSvgPath_Click);
            // 
            // btnTable
            // 
            this.btnTable.BackColor = System.Drawing.Color.Transparent;
            this.btnTable.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.btnTable.Location = new System.Drawing.Point(168, 33);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(150, 25);
            this.btnTable.TabIndex = 12;
            this.btnTable.Text = "Table";
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // navBarSingle
            // 
            this.navBarSingle.BackColor = System.Drawing.Color.Transparent;
            this.navBarSingle.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.navBarSingle.Location = new System.Drawing.Point(12, 33);
            this.navBarSingle.Name = "navBarSingle";
            this.navBarSingle.Size = new System.Drawing.Size(150, 25);
            this.navBarSingle.TabIndex = 10;
            this.navBarSingle.Text = "Navbar single-level";
            this.navBarSingle.Click += new System.EventHandler(this.navBarSingle_Click);
            // 
            // btnBoxShadow
            // 
            this.btnBoxShadow.BackColor = System.Drawing.Color.Transparent;
            this.btnBoxShadow.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.btnBoxShadow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoxShadow.Location = new System.Drawing.Point(324, 33);
            this.btnBoxShadow.Name = "btnBoxShadow";
            this.btnBoxShadow.Size = new System.Drawing.Size(150, 25);
            this.btnBoxShadow.TabIndex = 5;
            this.btnBoxShadow.Text = "Box Shadow Control";
            this.btnBoxShadow.Click += new System.EventHandler(this.btnBoxShadow_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.button3.Location = new System.Drawing.Point(168, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 25);
            this.button3.TabIndex = 3;
            this.button3.Text = "Controls";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BorderRadius = new Mmosoft.Oops.Controls.Buttons.BorderRadius(0, 0, 0, 0);
            this.button2.Location = new System.Drawing.Point(324, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "Layer control";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(490, 199);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.horizontalLine1);
            this.Controls.Add(this.btnSvgPath);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.navBarSingle);
            this.Controls.Add(this.btnBoxShadow);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPortal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPortal";
            this.ResumeLayout(false);

        }

        #endregion
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton button2;
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton button3;
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton btnBoxShadow;
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton navBarSingle;
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton btnTable;
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton btnSvgPath;
        private Mmosoft.Oops.Controls.Lines.HorizontalLine horizontalLine1;
        private Mmosoft.Oops.Controls.Buttons.BorderRadiusButton button5;
    }
}