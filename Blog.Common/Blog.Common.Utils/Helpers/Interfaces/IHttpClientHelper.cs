using System.Net.Http;

namespace Blog.Common.Utils.Helpers.Interfaces
{
    public interface IHttpClientHelper
    {
        string Get(string baseUri, string url);
        HttpResponseMessage HttpGet(string baseUri, string url);
        string Get(string baseUri, string url, string authenticationToken);
        HttpResponseMessage HttpGet(string baseUri, string url, string authenticationToken);

        string Post<T>(string baseUri, string url, T obj) where T : class;
        string Post<T>(string baseUri, string url, T obj, string authenticationToken) where T : class;

        string Put<T>(string baseUri, string url, T obj) where T : class;
        string Put<T>(string baseUri, string url, T obj, string authenticationToken) where T : class;

        string Delete(string baseUri, string url);
        string Delete(string baseUri, string url, string authenticationToken);
    }
}
