namespace Mmosoft.Oops.Controls
{
    partial class MediaController
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnHost = new System.Windows.Forms.Panel();
            this.tbDuration = new Mmosoft.Oops.TrackBar();
            this.fbLoop = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbNext = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbPlayPause = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbBack = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbShuffle = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.pnHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnHost
            // 
            this.pnHost.Controls.Add(this.tbDuration);
            this.pnHost.Controls.Add(this.fbLoop);
            this.pnHost.Controls.Add(this.fbNext);
            this.pnHost.Controls.Add(this.fbPlayPause);
            this.pnHost.Controls.Add(this.fbBack);
            this.pnHost.Controls.Add(this.fbShuffle);
            this.pnHost.Location = new System.Drawing.Point(0, 0);
            this.pnHost.Name = "pnHost";
            this.pnHost.Size = new System.Drawing.Size(306, 84);
            this.pnHost.TabIndex = 0;
            // 
            // tbDuration
            // 
            this.tbDuration.Location = new System.Drawing.Point(14, 11);
            this.tbDuration.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbDuration.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new System.Drawing.Size(278, 11);
            this.tbDuration.TabIndex = 11;
            this.tbDuration.Text = "trackBar1";
            this.tbDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // fbLoop
            // 
            this.fbLoop.BackColor = System.Drawing.Color.White;
            this.fbLoop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fbLoop.IconImage = null;
            this.fbLoop.Location = new System.Drawing.Point(252, 34);
            this.fbLoop.Name = "fbLoop";
            this.fbLoop.Size = new System.Drawing.Size(40, 40);
            this.fbLoop.TabIndex = 10;
            // 
            // fbNext
            // 
            this.fbNext.BackColor = System.Drawing.Color.White;
            this.fbNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fbNext.IconImage = null;
            this.fbNext.Location = new System.Drawing.Point(192, 34);
            this.fbNext.Name = "fbNext";
            this.fbNext.Size = new System.Drawing.Size(40, 40);
            this.fbNext.TabIndex = 9;
            // 
            // fbPlayPause
            // 
            this.fbPlayPause.BackColor = System.Drawing.Color.White;
            this.fbPlayPause.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fbPlayPause.IconImage = null;
            this.fbPlayPause.Location = new System.Drawing.Point(132, 34);
            this.fbPlayPause.Name = "fbPlayPause";
            this.fbPlayPause.Size = new System.Drawing.Size(40, 40);
            this.fbPlayPause.TabIndex = 8;
            // 
            // fbBack
            // 
            this.fbBack.BackColor = System.Drawing.Color.White;
            this.fbBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fbBack.IconImage = null;
            this.fbBack.Location = new System.Drawing.Point(74, 34);
            this.fbBack.Name = "fbBack";
            this.fbBack.Size = new System.Drawing.Size(40, 40);
            this.fbBack.TabIndex = 7;
            // 
            // fbShuffle
            // 
            this.fbShuffle.BackColor = System.Drawing.Color.White;
            this.fbShuffle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fbShuffle.IconImage = null;
            this.fbShuffle.Location = new System.Drawing.Point(14, 34);
            this.fbShuffle.Name = "fbShuffle";
            this.fbShuffle.Size = new System.Drawing.Size(40, 40);
            this.fbShuffle.TabIndex = 6;
            // 
            // MediaController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnHost);
            this.Name = "MediaController";
            this.Size = new System.Drawing.Size(306, 84);
            this.pnHost.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHost;
        private TrackBar tbDuration;
        private Buttons.FlatButton fbLoop;
        private Buttons.FlatButton fbNext;
        private Buttons.FlatButton fbPlayPause;
        private Buttons.FlatButton fbBack;
        private Buttons.FlatButton fbShuffle;
    }
}
