using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Admin.Web.Helpers;
using Blog.Common.Identity.Repository;
using Blog.Common.Identity.User;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using UserHelper = Blog.Admin.Web.Helpers.UserHelper;

namespace Blog.Admin.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Simple Injection Setup
            var container = new Container();

            // SI Controllers Dependency Injection
            container.Register<IUserHelper, UserHelper>(Lifestyle.Singleton);
            container.Register<IConfigurationHelper, ConfigurationHelper>(Lifestyle.Singleton);
            container.Register<IErrorSignaler, ErrorSignaler>(Lifestyle.Singleton);
            container.Register<IAddressResource, AddressResource>(Lifestyle.Singleton);
            container.Register<IUsersResource, UsersResource>(Lifestyle.Singleton);
            container.Register<INotificationResource, NotificationResource>(Lifestyle.Singleton);

            // SI Token Identity Registers
            container.Register<IdentityDbContext<BlogUser>, BlogIdentityDbContext>(Lifestyle.Singleton);
            container.Register<IUserStore<BlogUser>, BlogUserStore>(Lifestyle.Singleton);
            container.Register<IBlogDbRepository, BlogDbRepository>(Lifestyle.Singleton);
            container.Register<BlogUserManager, BlogUserManager>(Lifestyle.Singleton);

            // SI Registrations
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.EnableLifetimeScoping();
            container.Verify();

            // Register the dependency resolver.
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
