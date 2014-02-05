﻿using System.Net.Http.Formatting;
using BlogApi.Formatter;

namespace BlogApi.App_Start
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