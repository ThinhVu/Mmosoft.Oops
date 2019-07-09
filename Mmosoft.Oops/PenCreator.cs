using System.Drawing;

namespace Mmosoft.Oops
{
    public static class PenCreator
    {
        /// <summary>
        /// Create pen with specified color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Pen Create(string color = "#0", float lineWeight = 1f)
        {
            return new Pen(ExColorTranslator.Get(color));
        }

        public static Pen Create(Color color, float lineWeight = 1f)
        {
            return new Pen(color, lineWeight);
        }
    }
}
