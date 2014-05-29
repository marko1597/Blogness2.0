namespace Blog.Common.Utils.Helpers.Interfaces
{
    public interface IHttpClientHelper
    {
        string Get(string baseUri, string url);
        string Post<T>(string baseUri, string url, T obj) where T : class;
        string Put<T>(string baseUri, string url, T obj) where T : class;
    }
}
