using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions;
using SimpleInjector;

namespace Blog.Web.Site
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Allow Any Certificates
            // This should not be the same in Production
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            // Create the container as usual.
            var container = new Container();
            container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            container.Register<IErrorSignaler, ErrorSignaler>(Lifestyle.Singleton);
            container.Register<IHttpClientHelper, HttpClientHelper>(Lifestyle.Singleton);
            container.Register<IConfigurationHelper, ConfigurationHelper>(Lifestyle.Singleton);

            // SI Attributes Dependency Injection
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjector.Integration.Web.Mvc.SimpleInjectorDependencyResolver(container));
        }
    }
}
