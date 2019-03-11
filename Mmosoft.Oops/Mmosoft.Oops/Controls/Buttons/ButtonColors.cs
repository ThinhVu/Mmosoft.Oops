using System.ComponentModel;
using System.Drawing;
using C = Mmosoft.Oops.Controls.ColorTemplate.ButtonColor;

namespace Mmosoft.Oops.Controls.Buttons
{
    [System.Serializable]
    public class ButtonColors
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

        public Color BgFocused;
        public Color BorderFocused;
        public Color TextFocused;

        public ButtonColors()
        {            
            Bg = ExColorTranslator.Get(C.Bg);
            Border = ExColorTranslator.Get(C.Border);
            Text = ExColorTranslator.Get(C.Text);

            BgDisabled = ExColorTranslator.Get(C.BgDisabled);
            TextDisabled = ExColorTranslator.Get(C.TextDisabled);

            BgHovered = ExColorTranslator.Get(C.BgHovered);
            BorderHovered = ExColorTranslator.Get(C.BorderHovered);
            TextHovered = ExColorTranslator.Get(C.TextHovered);

            BgFocused = ExColorTranslator.Get(C.BgFocused);
            BorderFocused = ExColorTranslator.Get(C.BorderFocused);
            TextFocused = ExColorTranslator.Get(C.TextFocused);
        }
    }
}