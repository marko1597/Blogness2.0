using System.Web.Http;

namespace Blog.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());
        }
    }
}
