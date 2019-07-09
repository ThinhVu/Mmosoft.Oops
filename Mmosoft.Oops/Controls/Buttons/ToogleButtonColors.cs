using C = Mmosoft.Oops.Controls.ColorTemplate.ToogleButtonColors;

namespace Mmosoft.Oops.Colors
{
    public class ToogleButtonColors
    {
        // background color
        public string Bg;
        public string BgDisabled;
        public string BgHovered;
        public string BgChecked;

        // border color
        public string Border;
        public string BorderDisabled;
        public string BorderHovered;
        public string BorderChecked;

        // text color
        public string Dot;
        public string DotDisabled;
        public string DotHovered;
        public string DotChecked;

        public ToogleButtonColors()
        {
            Bg = C.Bg;
            BgDisabled = C.BgDisabled;
            BgHovered = C.BgHovered;
            BgChecked = C.BgChecked;

            // 
            Border = C.Border;
            BorderDisabled = C.BorderDisabled;
            BorderHovered = C.BorderHovered;
            BorderChecked = C.BorderChecked;

            // 
            Dot = C.Dot;
            DotDisabled = C.DotDisabled;
            DotHovered = C.DotHovered;
            DotChecked = C.DotChecked;       
        }
    }
}
