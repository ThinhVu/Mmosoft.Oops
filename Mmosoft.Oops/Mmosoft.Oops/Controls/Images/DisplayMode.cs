namespace Mmosoft.Oops.Controls
{
    public enum DisplayMode
    {
        //
        // Summary:
        //     The image is placed in the upper-left corner of the System.Windows.Forms.PictureBox.
        //     The image is clipped if it is larger than the System.Windows.Forms.PictureBox
        //     it is contained in.
        Normal = 0,
        //
        // Summary:
        //     The image within the viewport is stretched or shrunk to
        //     fit the size of the viewport.
        StretchImage = 1,
        //
        // Summary:
        //     The viewport is sized equal to the size of the image that
        //     it contains.
        AutoSize = 2,
        //
        // Summary:
        //     The image is displayed in the center if the viewport is larger than the image.
        //     If the image is larger than the viewport,
        //     the picture is placed in the center of the viewport and
        //     the outside edges are clipped.
        CenterImage = 3,
        //
        // Summary:
        //     The size of the image is increased or decreased maintaining the size ratio.
        //     Entire image will be shown in the viewport.
        ScaleLoss = 4,
        //
        // Sumary:
        //     The size of the image is increased or decreased maintaining the size ratio.
        //     An image will be shown in the viewport, outside edges are clipped
        ScaleLossLess = 5,
        //
        // Summary:
        //     The size of the image is not only increased or decreased maintaining the size ratio
        //     but also filled entire viewport. The outside edges are clipped.
        ScaleLossCenter = 7,
        //
        // Summary:
        //
        ScaleLossLessCenter = 8,

        ChangeHeightFixedWidthRatio = 9,

        ChangeWidthFixedHeightRatio = 10
    }
}
