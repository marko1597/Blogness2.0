using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Common.Utils.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        public string BaseUri { get; set; }

        private HttpClient _httpClientObj;
        public HttpClient HttpClientObj
        {
            get
            {
                if (_httpClientObj == null || _httpClientObj.BaseAddress.AbsoluteUri != BaseUri)
                {
                    _httpClientObj = new HttpClient { BaseAddress = new Uri(BaseUri) };
                    _httpClientObj.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return _httpClientObj;
            }
            set { _httpClientObj = value; }
        }

        public string Get(string baseUri, string url)
        {
            try
            {
                BaseUri = baseUri;
                return HttpClientObj.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public string Post<T>(string baseUri, string url, T obj) where T : class
        {
            try
            {
                BaseUri = baseUri;
                var result = HttpClientObj.PostAsJsonAsync(url, obj).Result.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public string Put<T>(string baseUri, string url, T obj) where T : class
        {
            try
            {
                BaseUri = baseUri;
                var result = HttpClientObj.PutAsJsonAsync(url, obj).Result.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
