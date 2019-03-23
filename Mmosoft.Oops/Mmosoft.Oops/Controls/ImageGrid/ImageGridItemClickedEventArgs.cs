using System;
using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public delegate void ImageGridItemClickedEventHandler(object sender, ImageGridItemClickedEventArgs e);

    public class ImageGridItemClickedEventArgs : EventArgs
    {
        public Image Image { get; set; }
        public int Index { get; set; }
    }
}
