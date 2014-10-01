using System;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Common.Utils.Helpers
{
    public class PropertyReflection : IPropertyReflection
    {
        public bool HasProperty<T>(T src, string property) where T : class
        {

            var type = src.GetType();
            var prop = type.GetProperty(property);

            return prop != null;
        }

        public object GetPropertyValue<T>(T src, string property) where T : class
        {
            var type = src.GetType();
            var propertyInfo = type.GetProperty(property);
            if (propertyInfo == null) throw new Exception(
                string.Format("Property {0} does not exist on {1} object", property, type.Name));

            return propertyInfo.GetValue(src, null);
        }

        public void SetProperty<T>(T src, string property, object value) where T : class
        {
            var type = src.GetType();
            var propertyInfo = type.GetProperty(property);
            if (propertyInfo == null) throw new Exception(
                string.Format("Property {0} does not exist on {1} object", property, type.Name));

            propertyInfo.SetValue(src, value, null);
        }
    }
}
