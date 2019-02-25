using System.ComponentModel;
using System.Drawing;
using C = Mmosoft.Oops.Controls.ColorTemplate.ButtonColor;

namespace Mmosoft.Oops.Controls.Buttons
{
    [System.Serializable]
    public class BorderRaidusButtonColors
    {
        public Color Bg;
        public Color Border;
        public Color Text;

        public Color BgDisabled;
        public Color BorderDisabled;
        public Color TextDisabled;

        public Color BgHovered;
        public Color BorderHovered;
        public Color TextHovered;

        public BorderRaidusButtonColors()
        {            
            Bg = CustomColorTranslator.Get(C.Bg);
            Border = CustomColorTranslator.Get(C.Border);
            Text = CustomColorTranslator.Get(C.Text);

            BgDisabled = CustomColorTranslator.Get(C.BgDisabled);
            BorderDisabled = CustomColorTranslator.Get(C.BorderDisabled);
            TextDisabled = CustomColorTranslator.Get(C.TextDisabled);

            BgHovered = CustomColorTranslator.Get(C.BgHovered);
            BorderHovered = CustomColorTranslator.Get(C.BorderHovered);
            TextHovered = CustomColorTranslator.Get(C.TextHovered);
        }
    }
}