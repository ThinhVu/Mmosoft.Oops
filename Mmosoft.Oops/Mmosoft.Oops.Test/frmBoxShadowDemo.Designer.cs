namespace Mmosoft.OopsTest
{
    partial class frmBoxShadowDemo
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
            this.card2 = new Mmosoft.Oops.Controls.Card();
            this.card1 = new Mmosoft.Oops.Controls.Card();
            this.card4 = new Mmosoft.Oops.Controls.Card();
            this.card3 = new Mmosoft.Oops.Controls.Card();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hover over these picture";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.card2.Image = global::Mmosoft.Oops.Test.Properties.Resources.girl1;
            this.card2.Location = new System.Drawing.Point(334, 42);
            this.card2.Name = "card2";
            this.card2.Shadow = 0;
            this.card2.Size = new System.Drawing.Size(157, 222);
            this.card2.TabIndex = 2;
            this.card2.Text = "card2";
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.card1.Image = global::Mmosoft.Oops.Test.Properties.Resources.girl2;
            this.card1.Location = new System.Drawing.Point(12, 42);
            this.card1.Name = "card1";
            this.card1.Shadow = 0;
            this.card1.Size = new System.Drawing.Size(313, 222);
            this.card1.TabIndex = 1;
            this.card1.Text = "card1";
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.Transparent;
            this.card4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.card4.Image = global::Mmosoft.Oops.Test.Properties.Resources.girl2;
            this.card4.Location = new System.Drawing.Point(177, 275);
            this.card4.Name = "card4";
            this.card4.Shadow = 0;
            this.card4.Size = new System.Drawing.Size(313, 222);
            this.card4.TabIndex = 5;
            this.card4.Text = "card4";
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.Transparent;
            this.card3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.card3.Image = global::Mmosoft.Oops.Test.Properties.Resources.girl1;
            this.card3.Location = new System.Drawing.Point(12, 275);
            this.card3.Name = "card3";
            this.card3.Shadow = 0;
            this.card3.Size = new System.Drawing.Size(157, 222);
            this.card3.TabIndex = 6;
            this.card3.Text = "card3";
            // 
            // frmBoxShadow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(533, 518);
            this.Controls.Add(this.card3);
            this.Controls.Add(this.card4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card1);
            this.DoubleBuffered = true;
            this.Name = "frmBoxShadow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Box Shadow";
            this.Load += new System.EventHandler(this.frmIconMgrLab_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Mmosoft.Oops.Controls.Card card1;
        private Mmosoft.Oops.Controls.Card card2;
        private System.Windows.Forms.Label label1;
        private Mmosoft.Oops.Controls.Card card4;
        private Mmosoft.Oops.Controls.Card card3;
    }
}