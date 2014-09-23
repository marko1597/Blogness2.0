using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Admin.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "UserAddress",
                "Users/Details/{userId}/Address",
                new { controller = "Address", action = "Index" }
            );

            routes.MapRoute(
                "UserHobbies",
                "Users/Details/{userId}/Hobbies",
                new { controller = "Hobbies", action = "Index" }
            );

            routes.MapRoute(
                "UserEducation",
                "Users/Details/{userId}/Education",
                new { controller = "Education", action = "Index" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
