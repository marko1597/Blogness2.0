using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Implementation;
using SimpleInjector;

namespace Blog.Backend.Api.Rest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FormatConfig.RegisterFormats(GlobalConfiguration.Configuration.Formatters);

            // Simple Injection Setup
            var container = new Container();
            container.Register<IComments, Comments>(Lifestyle.Singleton);
            container.Register<ICommentLikes, CommentLikes>(Lifestyle.Singleton);
            container.Register<IPosts, Posts>(Lifestyle.Singleton);
            container.Register<IPostsPage, PostsPage>(Lifestyle.Singleton);
            container.Register<IUser, Users>(Lifestyle.Singleton);
            container.Register<ISession, Session>(Lifestyle.Singleton);

            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcAttributeFilterProvider();

            container.EnableLifetimeScoping();
            container.Verify();

            // Register the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}