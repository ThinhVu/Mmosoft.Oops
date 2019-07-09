using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops.Controls
{
    public enum ImageGridDisplayMode
    {
        //
        // Summary:
        //     The image within the viewport is stretched or shrunk to
        //     fit the size of the viewport.
        StretchImage = 0,
        //
        // Summary:
        //     The size of the image is not only increased or decreased maintaining the size ratio
        //     but also filled entire viewport. The outside edges are clipped.
        ScaleLossCenter = 1,
    }
}
