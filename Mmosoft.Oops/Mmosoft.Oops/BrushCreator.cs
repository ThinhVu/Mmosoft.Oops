using System.Drawing;

namespace Mmosoft.Oops
{    
    public static class BrushCreator
    {       
        public static SolidBrush CreateSolidBrush(string color = "#0")
        {
            return new SolidBrush(ExColorTranslator.Get(color));
        }

        public static SolidBrush CreateSolidBrush(Color color)
        {
            return new SolidBrush(color);
        }
    }
}