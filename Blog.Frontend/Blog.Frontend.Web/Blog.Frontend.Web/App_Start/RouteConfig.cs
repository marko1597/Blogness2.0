using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Frontend.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PostsPage", action = "PopularPosts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Parameterized",
                url: "{controller}/{id}/{action}",
                defaults: new { controller = "PostsPage", action = "PopularPosts", id = UrlParameter.Optional }
            );
        }
    }
}