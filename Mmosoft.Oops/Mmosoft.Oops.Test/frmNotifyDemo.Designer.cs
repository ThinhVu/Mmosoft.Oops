namespace Mmosoft.Oops.Test
{
    partial class frmNotifyDemo
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
            this.btnDanger = new Mmosoft.Oops.Controls.Buttons.Button();
            this.btnWarning = new Mmosoft.Oops.Controls.Buttons.Button();
            this.btnInfor = new Mmosoft.Oops.Controls.Buttons.Button();
            this.SuspendLayout();
            // 
            // btnDanger
            // 
            this.btnDanger.Location = new System.Drawing.Point(571, 175);
            this.btnDanger.Name = "btnDanger";
            this.btnDanger.Size = new System.Drawing.Size(75, 23);
            this.btnDanger.TabIndex = 2;
            this.btnDanger.Text = "Danger";
            this.btnDanger.Click += new System.EventHandler(this.btnDanger_Click);
            // 
            // btnWarning
            // 
            this.btnWarning.Location = new System.Drawing.Point(490, 175);
            this.btnWarning.Name = "btnWarning";
            this.btnWarning.Size = new System.Drawing.Size(75, 23);
            this.btnWarning.TabIndex = 1;
            this.btnWarning.Text = "Warning";
            this.btnWarning.Click += new System.EventHandler(this.btnWarning_Click);
            // 
            // btnInfor
            // 
            this.btnInfor.Location = new System.Drawing.Point(409, 175);
            this.btnInfor.Name = "btnInfor";
            this.btnInfor.Size = new System.Drawing.Size(75, 23);
            this.btnInfor.TabIndex = 0;
            this.btnInfor.Text = "Infor";
            this.btnInfor.Click += new System.EventHandler(this.btnInfor_Click);
            // 
            // frmNotifyDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 210);
            this.Controls.Add(this.btnDanger);
            this.Controls.Add(this.btnWarning);
            this.Controls.Add(this.btnInfor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNotifyDemo";
            this.Text = "frmNotifyDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Buttons.Button btnInfor;
        private Controls.Buttons.Button btnWarning;
        private Controls.Buttons.Button btnDanger;
    }
}