using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Common.Utils;
using Blog.Common.Web.Authentication;
using Blog.Common.Web.Helper;
using Blog.Services.Implementation;
using Blog.Services.Implementation.Interfaces;
using SimpleInjector;

namespace Blog.Web.Api
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
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SslValidator.OverrideValidation();
            
            // Simple Injection Setup
            var container = new Container();
            container.Register<IComments, CommentsService>(Lifestyle.Singleton);
            container.Register<ICommentLikes, CommentLikesService>(Lifestyle.Singleton);
            container.Register<IPosts, PostsService>(Lifestyle.Singleton);
            container.Register<IPostsPage, PostsPageService>(Lifestyle.Singleton);
            container.Register<IUser, UsersService>(Lifestyle.Singleton);
            container.Register<ISession, SessionService>(Lifestyle.Singleton);
            container.Register<IPostLikes, PostLikesService>(Lifestyle.Singleton);
            container.Register<IPostContents, PostContentsService>(Lifestyle.Singleton);
            container.Register<IMedia, MediaService>(Lifestyle.Singleton);
            container.Register<IAlbum, AlbumService>(Lifestyle.Singleton);
            container.Register<IEducation, EducationService>(Lifestyle.Singleton);
            container.Register<IHobby, HobbyService>(Lifestyle.Singleton);
            container.Register<IAddress, AddressService>(Lifestyle.Singleton);
            container.Register<IImageHelper, ImageHelper>(Lifestyle.Singleton);
            container.Register<ITag, TagsService>(Lifestyle.Singleton);
            container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);

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