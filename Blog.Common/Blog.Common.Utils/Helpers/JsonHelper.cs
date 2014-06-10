using Newtonsoft.Json;

namespace Blog.Common.Utils.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeJson<T>(T obj)
        {
            if (Equals(obj, null))
                return "";

            var r = JsonConvert.SerializeObject(obj);
            return r;
        }

        public static T DeserializeJson<T>(string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}