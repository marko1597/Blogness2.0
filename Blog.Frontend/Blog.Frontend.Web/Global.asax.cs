using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Backend.Common.Utils;
using Blog.Backend.Common.Web.Authentication;
using SimpleInjector;

namespace Blog.Frontend.Web
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
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            SslValidator.OverrideValidation();

            // Create the container as usual.
            var container = new Container();
            container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcAttributeFilterProvider();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjector.Integration.Web.Mvc.SimpleInjectorDependencyResolver(container));
        }
    }
}
