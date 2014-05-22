using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Blog.Common.Web.Helper
{
    public class HttpClientHelper
    {
        private static HttpClient _httpClient;

        public HttpClientHelper(string url)
        {
            if (_httpClient == null || _httpClient.BaseAddress.AbsoluteUri != url)
            {
                _httpClient = new HttpClient {BaseAddress = new Uri(url)};
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public string Get(string url)
        {
            return _httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public string Post<T>(string url, T obj) where T : class
        {
            HttpContent content = new StringContent(JsonHelper.SerializeJson(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = _httpClient.PostAsJsonAsync(url, obj).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string Put<T>(string url, T obj) where T : class
        {
            HttpContent content = new StringContent(JsonHelper.SerializeJson(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = _httpClient.PutAsJsonAsync(url, obj).Result.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
