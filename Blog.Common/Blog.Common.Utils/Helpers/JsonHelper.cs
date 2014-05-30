using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Blog.Common.Utils.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeJson<T>(T obj)
        {
            if (Equals(obj, null))
                return "";

            var r = "";
            try
            {
                r = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("#ERR SerializeJson:\t" + ex.Message);
            }
            return r;
        }

        public static T DeserializeJson<T>(string json)
        {
            var obj = default(T);
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
               Debug.WriteLine("#ERR DeserializeJson:\t" + ex.Message);
            }
            return obj;
        }
    }
}