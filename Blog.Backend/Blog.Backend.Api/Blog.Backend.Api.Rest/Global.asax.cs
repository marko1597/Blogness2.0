using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Backend.Common.Utils;
using Blog.Backend.Common.Web.Authentication;
using Blog.Backend.Common.Web.Helper;
using Blog.Backend.Services.Implementation;
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
            container.Register<IPostContents, PostContentsService>(Lifestyle.Singleton);
            container.Register<IMedia, MediaService>(Lifestyle.Singleton);
            container.Register<IAlbum, AlbumService>(Lifestyle.Singleton);
            container.Register<IEducation, EducationService>(Lifestyle.Singleton);
            container.Register<IHobby, HobbyService>(Lifestyle.Singleton);
            container.Register<IAddress, AddressService>(Lifestyle.Singleton);
            container.Register<IImageHelper, ImageHelper>(Lifestyle.Singleton);
            container.Register<ITag, TagsService>(Lifestyle.Singleton);
            container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);

            /*
             * Uncomment these lines to switch between mocks or real db calls
             * -------------------------------------------------------------- */
            //DataStorage.LoadMockData();
            //container.Register<IComments, CommentMock>(Lifestyle.Singleton);
            //container.Register<ICommentLikes, CommentLikeMock>(Lifestyle.Singleton);
            //container.Register<IPosts, PostMock>(Lifestyle.Singleton);
            //container.Register<IPostsPage, PostPageMock>(Lifestyle.Singleton);
            //container.Register<IUser, UserMock>(Lifestyle.Singleton);
            //container.Register<ISession, SessionMock>(Lifestyle.Singleton);
            //container.Register<IPostContents, PostContentMock>(Lifestyle.Singleton);
            //container.Register<IMedia, MediaMock>(Lifestyle.Singleton);
            //container.Register<IAlbum, AlbumMock>(Lifestyle.Singleton);
            //container.Register<IEducation, EducationMock>(Lifestyle.Singleton);
            //container.Register<IHobby, HobbyMock>(Lifestyle.Singleton);
            //container.Register<IAddress, AddressMock>(Lifestyle.Singleton);
            //container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);

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