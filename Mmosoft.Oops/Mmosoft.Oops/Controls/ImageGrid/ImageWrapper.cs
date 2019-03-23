using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops.Controls
{
    class ImageWrapper
    {
        public Image Image { get; set; }
        public Rectangle Boundary { get; set; }

        public ImageWrapper(Image img)
        {
            Image = img;
        }
    }
}