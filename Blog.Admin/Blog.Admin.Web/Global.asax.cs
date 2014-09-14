﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Common.Identity;
using Blog.Common.Identity.Interfaces;
using Blog.Services.Helpers.Wcf;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

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
