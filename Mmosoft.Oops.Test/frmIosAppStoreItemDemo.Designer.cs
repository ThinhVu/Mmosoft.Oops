namespace Mmosoft.Oops.Test
{
    partial class frmIosAppStoreItemDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIosAppStoreItemDemo));
            this.iosAppStoreItemControl1 = new Mmosoft.Oops.Controls.iosAppStoreItem.iosAppStoreItemControl();
            this.SuspendLayout();
            // 
            // iosAppStoreItemControl1
            // 
            this.iosAppStoreItemControl1.BackColor = System.Drawing.Color.White;
            this.iosAppStoreItemControl1.Content = resources.GetString("iosAppStoreItemControl1.Content");
            this.iosAppStoreItemControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iosAppStoreItemControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iosAppStoreItemControl1.ItemImage = ((System.Drawing.Image)(resources.GetObject("iosAppStoreItemControl1.ItemImage")));
            this.iosAppStoreItemControl1.Location = new System.Drawing.Point(0, 0);
            this.iosAppStoreItemControl1.Name = "iosAppStoreItemControl1";
            this.iosAppStoreItemControl1.Size = new System.Drawing.Size(330, 593);
            this.iosAppStoreItemControl1.TabIndex = 0;
            this.iosAppStoreItemControl1.Text = "iosAppStoreItemControl1";
            // 
            // frmIosAppStoreItemDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 593);
            this.Controls.Add(this.iosAppStoreItemControl1);
            this.Name = "frmIosAppStoreItemDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmIosAppStoreItemDemo";
            this.Load += new System.EventHandler(this.frmIosAppStoreItemDemo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.iosAppStoreItem.iosAppStoreItemControl iosAppStoreItemControl1;
    }
}