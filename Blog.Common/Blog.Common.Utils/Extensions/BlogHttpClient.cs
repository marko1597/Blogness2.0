using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Blog.Common.Utils.Extensions
{
    public class BlogHttpClient : HttpClient
    {
        public BlogHttpClient()
        {
        }

        /// <summary>
        /// Bloggity's HttpClient object
        /// </summary>
        /// <param name="uri">Base uri for the HttpClient resource</param>
        /// <param name="authenticationToken">Authentication header token</param>
        public BlogHttpClient(string uri, string authenticationToken = null)
        {
            if (BaseAddress.AbsoluteUri == uri) return;

            BaseAddress = new Uri(uri);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authenticationToken))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(string.Format("Bearer {0}", authenticationToken));
            }
        }
    }
}
