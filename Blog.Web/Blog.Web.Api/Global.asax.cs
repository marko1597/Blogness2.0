using System.Net;
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
using Blog.Services.Helpers.Wcf;
using Blog.Services.Helpers.Wcf.Interfaces;
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

            // Allow Any Certificates
            // This should not be the same in Production
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            
            // Simple Injection Setup
            var container = new Container();
            container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();

            // SI Controllers Dependency Injection
            container.Register<ICommentsResource, CommentsResource>(Lifestyle.Singleton);
            container.Register<ICommentLikesResource, CommentLikesResource>(Lifestyle.Singleton);
            container.Register<IPostsResource, PostsResource>(Lifestyle.Singleton);
            container.Register<IUsersResource, UsersResource>(Lifestyle.Singleton);
            container.Register<ISessionResource, SessionResource>(Lifestyle.Singleton);
            container.Register<IPostLikesResource, PostLikesResource>(Lifestyle.Singleton);
            container.Register<IPostContentsResource, PostContentsResource>(Lifestyle.Singleton);
            container.Register<IMediaResource, MediaResource>(Lifestyle.Singleton);
            container.Register<IAlbumResource, AlbumResource>(Lifestyle.Singleton);
            container.Register<IEducationResource, EducationResource>(Lifestyle.Singleton);
            container.Register<IHobbyResource, HobbyResource>(Lifestyle.Singleton);
            container.Register<IAddressResource, AddressResource>(Lifestyle.Singleton);
            container.Register<IImageHelper, ImageHelper>(Lifestyle.Singleton);
            container.Register<ITagsResource, TagsResource>(Lifestyle.Singleton);
            container.Register<IErrorSignaler, ErrorSignaler>(Lifestyle.Singleton);
            container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);
            container.Register<IHttpClientHelper, HttpClientHelper>(Lifestyle.Singleton);
            container.Register<IConfigurationHelper, ConfigurationHelper>(Lifestyle.Singleton);

            // SI Attributes Dependency Injection
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.Session = container.GetInstance<SessionResource>());
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.Session = container.GetInstance<SessionResource>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());

            // SI Helper Classes Property Injections
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