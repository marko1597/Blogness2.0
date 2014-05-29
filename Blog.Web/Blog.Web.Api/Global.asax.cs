using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Common.Web.Extensions;
using Blog.Common.Web.Extensions.Elmah;
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
            container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();

            // SI Controllers Dependency Injection
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
            container.Register<IErrorSignaler, ErrorSignaler>(Lifestyle.Singleton);
            container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);
            container.Register<IHttpClientHelper, HttpClientHelper>(Lifestyle.Singleton);
            container.Register<IConfigurationHelper, ConfigurationHelper>(Lifestyle.Singleton);

            // SI Attributes Dependency Injection
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.Session = container.GetInstance<SessionService>());
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.Session = container.GetInstance<SessionService>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());

            //// SI Helper Classes Property Injections
            container.RegisterInitializer<AuthenticationHelper>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());

            // SI Registrations
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            container.EnableLifetimeScoping();
            container.Verify();

            // Register the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}