using System;
using System.Reflection;

namespace Mmosoft.Oops.Controls.Table
{
    public class PropertyDataReader
    {
        private PropertyInfo[] propInfors;
        
        public PropertyDataReader(Type objType)
        {
            this.propInfors = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public object GetData(object o, string name)
        {
            foreach (PropertyInfo prop in propInfors)
            {
                if (prop.Name == name)
                {
                    return prop.GetValue(o, null);
                }
            }
            throw new Exception("Propery not found: " + name);
        }
    }
}
