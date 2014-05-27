﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Common.Utils.Helpers;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Common.Web.Extensions;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation;
using Blog.Services.Implementation.Interfaces;
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
            // ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            SslValidator.OverrideValidation();

            // Create the container as usual.
            var container = new Container();
            container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            container.Register<IAuthenticationHelper, AuthenticationHelper>(Lifestyle.Singleton);
            container.Register<ISession, SessionRemoteService>(Lifestyle.Singleton);
            container.Register<IErrorSignaler, ErrorSignaler>(Lifestyle.Singleton);

            // SI Attributes Dependency Injection
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.Session = container.GetInstance<SessionRemoteService>());
            container.RegisterInitializer<BlogApiAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.Session = container.GetInstance<SessionRemoteService>());
            container.RegisterInitializer<BlogAuthorizationAttribute>(a => a.ErrorSignaler = container.GetInstance<ErrorSignaler>());

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcAttributeFilterProvider();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjector.Integration.Web.Mvc.SimpleInjectorDependencyResolver(container));
        }
    }
}