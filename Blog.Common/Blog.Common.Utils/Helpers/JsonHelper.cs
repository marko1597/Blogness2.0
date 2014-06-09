using System;
using Blog.Common.Utils.Extensions;
using Newtonsoft.Json;

namespace Blog.Common.Utils.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeJson<T>(T obj)
        {
            if (Equals(obj, null))
                return "";

            string r;
            try
            {
                r = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return r;
        }

        public static T DeserializeJson<T>(string json)
        {
            T obj;
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return obj;
        }
    }
}