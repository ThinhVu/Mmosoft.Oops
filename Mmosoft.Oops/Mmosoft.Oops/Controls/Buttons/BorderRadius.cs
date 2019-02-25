using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Mmosoft.Oops.Controls.Buttons
{
    [Serializable]
    [TypeConverter(typeof(BorderRadiusConverter))]
    public struct BorderRadius
    {
        private int topLeft;
        public int TopLeft { get { return topLeft; } }

        private int topRight;
        public int TopRight { get { return topRight; } }

        private int bottomRight;
        public int BottomRight { get { return bottomRight; } }

        private int bottomLeft;
        public int BottomLeft { get { return bottomLeft; } }        

        public BorderRadius(int tl, int tr, int br, int bl)
        {
            topLeft = tl;
            topRight = tr;
            bottomLeft = bl;
            bottomRight = br;
        }        
    }

    public class BorderRadiusConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is BorderRadius)
            {
                if (destinationType == typeof(InstanceDescriptor))
                {
                    BorderRadius br = (BorderRadius)value;

                    ConstructorInfo ctor = typeof(BorderRadius).GetConstructor(new Type[] {
                        typeof(int), typeof(int), typeof(int), typeof(int)
                    });
                    if (ctor != null)
                        return new InstanceDescriptor(ctor, new object[] { br.TopLeft, br.TopRight, br.BottomRight, br.BottomLeft });
                }
            }            
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            BorderRadius br = default(BorderRadius);
            br = new BorderRadius(
                    (int)propertyValues["TopLeft"],
                    (int)propertyValues["TopRight"],
                    (int)propertyValues["BottomLeft"],
                    (int)propertyValues["BottomRight"]);
            return br;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            BorderRadius br = new BorderRadius();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(BorderRadius), attributes);
            return props.Sort(new string[] { "TopLeft", "TopRight", "BottomRight", "BottomLeft" });
        }
    }
}