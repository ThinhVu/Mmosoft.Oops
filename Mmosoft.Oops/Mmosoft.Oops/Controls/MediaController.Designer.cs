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
            this.tbDuration = new Mmosoft.Oops.TrackBar();
            this.fbLoop = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbNext = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbPlayPause = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbBack = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.fbShuffle = new Mmosoft.Oops.Controls.Buttons.FlatButton();
            this.SuspendLayout();
            // 
            // tbDuration
            // 
            this.tbDuration.Location = new System.Drawing.Point(13, 3);
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
            this.tbDuration.TabIndex = 5;
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
            this.fbLoop.Image = null;
            this.fbLoop.Location = new System.Drawing.Point(251, 26);
            this.fbLoop.MouseEnterColor = System.Drawing.Color.Empty;
            this.fbLoop.Name = "fbLoop";
            this.fbLoop.Size = new System.Drawing.Size(40, 40);
            this.fbLoop.TabIndex = 4;
            // 
            // fbNext
            // 
            this.fbNext.BackColor = System.Drawing.Color.White;
            this.fbNext.Image = null;
            this.fbNext.Location = new System.Drawing.Point(191, 26);
            this.fbNext.MouseEnterColor = System.Drawing.Color.Empty;
            this.fbNext.Name = "fbNext";
            this.fbNext.Size = new System.Drawing.Size(40, 40);
            this.fbNext.TabIndex = 3;
            // 
            // fbPlayPause
            // 
            this.fbPlayPause.BackColor = System.Drawing.Color.White;
            this.fbPlayPause.Image = null;
            this.fbPlayPause.Location = new System.Drawing.Point(131, 26);
            this.fbPlayPause.MouseEnterColor = System.Drawing.Color.Empty;
            this.fbPlayPause.Name = "fbPlayPause";
            this.fbPlayPause.Size = new System.Drawing.Size(40, 40);
            this.fbPlayPause.TabIndex = 2;
            // 
            // fbBack
            // 
            this.fbBack.BackColor = System.Drawing.Color.White;
            this.fbBack.Image = null;
            this.fbBack.Location = new System.Drawing.Point(73, 26);
            this.fbBack.MouseEnterColor = System.Drawing.Color.Empty;
            this.fbBack.Name = "fbBack";
            this.fbBack.Size = new System.Drawing.Size(40, 40);
            this.fbBack.TabIndex = 1;
            // 
            // fbShuffle
            // 
            this.fbShuffle.BackColor = System.Drawing.Color.White;
            this.fbShuffle.Image = null;
            this.fbShuffle.Location = new System.Drawing.Point(13, 26);
            this.fbShuffle.MouseEnterColor = System.Drawing.Color.Empty;
            this.fbShuffle.Name = "fbShuffle";
            this.fbShuffle.Size = new System.Drawing.Size(40, 40);
            this.fbShuffle.TabIndex = 0;
            // 
            // MediaController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbDuration);
            this.Controls.Add(this.fbLoop);
            this.Controls.Add(this.fbNext);
            this.Controls.Add(this.fbPlayPause);
            this.Controls.Add(this.fbBack);
            this.Controls.Add(this.fbShuffle);
            this.Name = "MediaController";
            this.Size = new System.Drawing.Size(306, 84);
            this.ResumeLayout(false);

        }

        #endregion

        private Buttons.FlatButton fbShuffle;
        private Buttons.FlatButton fbBack;
        private Buttons.FlatButton fbPlayPause;
        private Buttons.FlatButton fbNext;
        private Buttons.FlatButton fbLoop;
        private TrackBar tbDuration;
    }
}
