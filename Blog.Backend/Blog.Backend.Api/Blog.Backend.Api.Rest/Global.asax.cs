using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Backend.Services.Implementation;
using Newtonsoft.Json;
using SimpleInjector;

namespace Blog.Backend.Api.Rest
{
    public class WebApiApplication : HttpApplication
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
            container.Register<IComments, CommentsService>(Lifestyle.Singleton);
            container.Register<ICommentLikes, CommentLikesService>(Lifestyle.Singleton);
            container.Register<IPosts, PostsService>(Lifestyle.Singleton);
            container.Register<IPostsPage, PostsPageService>(Lifestyle.Singleton);
            container.Register<IUser, UsersService>(Lifestyle.Singleton);
            container.Register<ISession, SessionService>(Lifestyle.Singleton);
            container.Register<IPostContents, PostContentsService>(Lifestyle.Singleton);
            container.Register<IMedia, MediaService>(Lifestyle.Singleton);

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