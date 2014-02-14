using System.Net.Http.Formatting;
using Blog.Backend.Api.Rest.Formatter;

namespace Blog.Backend.Api.Rest
{
    public static class FormatConfig
    {
        public static void RegisterFormats(MediaTypeFormatterCollection formatting)
        {
            var jsonp = new JsonpMediaTypeFormatter();
            jsonp.AddQueryStringMapping("format", "jsonp", "application/json");

            formatting.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            formatting.JsonFormatter.AddQueryStringMapping("format", "json", "application/json");
            formatting.Add(jsonp);
        }
    }
}