using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public partial class MediaController : UserControl
    {
        public MediaController()
        {
            InitializeComponent();
            tbDuration.MaxValue = 100;
            tbDuration.Value = 20;
            tbDuration.MinValue = 1;
            
            var sm = System.Drawing.Drawing2D.SmoothingMode.Default;
            fbShuffle.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Random, 4, Brushes.Black, sm);
            fbBack.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.MediaStepBackward, 4, Brushes.Black, sm);
            fbPlayPause.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.MediaPlay, 4, Brushes.Black, sm);
            fbNext.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.MediaStepForward, 4, Brushes.Black, sm);
            fbLoop.Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.Loop, 4, Brushes.Black, sm);
        }
    }
}
