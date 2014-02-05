using System.Web.Http;

namespace Blog.Backend.Api.Rest.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "ApiById",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: new { id = @"^[0-9]+$" }
            //);

            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());

            /*var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);*/
        }
    }
}
