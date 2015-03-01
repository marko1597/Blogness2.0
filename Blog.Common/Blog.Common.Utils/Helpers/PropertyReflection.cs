using System;
using System.Collections;
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
        
        public object GetPropertyValue<T>(T src, string property, bool isRecursive) where T : class
        {
            var type = src.GetType();
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name == property)
                {
                    var propertyInfo = type.GetProperty(property);
                    if (propertyInfo == null) throw new Exception(
                        string.Format("Cannot get {0} property value on {1} object", prop, type.Name));

                    return propertyInfo.GetValue(src, null);
                }

                var propValue = GetPropertyValue(src, prop.Name);

                if (propValue == null
                    || propValue is DateTime
                    || propValue is string
                    || propValue.GetType().IsPrimitive
                    || propValue is IEnumerable
                    || propValue.GetType().IsArray) continue;

                var recursiveResult = GetPropertyValue(propValue, property, true);

                if (recursiveResult != null) return recursiveResult;
            }

            return null;
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
