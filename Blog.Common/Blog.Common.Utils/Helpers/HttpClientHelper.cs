using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Common.Utils.Helpers
{
    public class HttpClientHelper : IHttpClientHelper, IDisposable
    {
        public string BaseUri { get; set; }

        public string AuthenticationToken { get; set; }

        private bool _disposed;

        private HttpClient _httpClientObj;
        public HttpClient HttpClientObj
        {
            get
            {
                if (_httpClientObj == null || _httpClientObj.BaseAddress.AbsoluteUri != BaseUri)
                {
                    _httpClientObj = new HttpClient { BaseAddress = new Uri(BaseUri) };
                    _httpClientObj.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(AuthenticationToken))
                    {
                        _httpClientObj.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(string.Format("Bearer {0}", AuthenticationToken));
                    }
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

        public HttpResponseMessage HttpGet(string baseUri, string url)
        {
            try
            {
                BaseUri = baseUri;
                return HttpClientObj.GetAsync(url).Result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public string Get(string baseUri, string url, string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
            return Get(baseUri, url);
        }

        public HttpResponseMessage HttpGet(string baseUri, string url, string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
            return HttpGet(baseUri, url);
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

        public string Post<T>(string baseUri, string url, T obj, string authenticationToken) where T : class
        {
            AuthenticationToken = authenticationToken;
            return Post(baseUri, url, obj);
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

        public string Put<T>(string baseUri, string url, T obj, string authenticationToken) where T : class
        {
            AuthenticationToken = authenticationToken;
            return Put(baseUri, url, obj);
        }

        public string Delete(string baseUri, string url)
        {
            try
            {
                BaseUri = baseUri;
                var result = HttpClientObj.DeleteAsync(url).Result.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public string Delete(string baseUri, string url, string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
            return Delete(baseUri, url);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (HttpClientObj != null)
                {
                    HttpClientObj.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
