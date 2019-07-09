using System;
using System.Collections.Generic;

namespace Mmosoft.Oops.Controls.Table
{
    [Serializable]
    public class Column : IComparer<object>
    {
        public string Title { get; set; }
        public string MappingProperty { get; set; }
        public string Format { get; set; }
        public int Width { get; set; }

        public Column()
        {
            Title = "";
            MappingProperty = "";
            Format = "{0}";
            Width = 100;
        }
        public int Compare(object x, object y)
        {
            var typeX = x.GetType();
            var compareProp = typeX.GetProperty(MappingProperty);

            dynamic xValue = compareProp.GetValue(x, null);
            dynamic yValue = compareProp.GetValue(y, null);
            
            if (compareProp.PropertyType == typeof(String))
            {
                return string.Compare(xValue, yValue);
            }
            else
            {
                // TODO: Deal with un-compareable type
                return xValue >= yValue ? 1 : -1;
            }            
        }
    }
}