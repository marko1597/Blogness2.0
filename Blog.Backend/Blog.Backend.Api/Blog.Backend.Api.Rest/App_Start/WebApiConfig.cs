using System.Web.Http;
using System.Web.Http.Cors;

namespace Blog.Backend.Api.Rest.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());
            /*var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);*/

        }
    }
}
