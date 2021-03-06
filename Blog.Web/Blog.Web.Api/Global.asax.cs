﻿using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Common.Identity.Repository;
using Blog.Common.Identity.User;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace Blog.Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FormatConfig.RegisterFormats(GlobalConfiguration.Configuration.Formatters);

            // Allow Any Certificates
            // This should not be the same in Production
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            
            // Simple Injection Setup
            var container = new Container();
            container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();

            // SI Controllers registry
            container.Register<ICommentsResource, CommentsResource>(Lifestyle.Singleton);
            container.Register<ICommunityResource, CommunityResource>(Lifestyle.Singleton);
            container.Register<ICommentLikesResource, CommentLikesResource>(Lifestyle.Singleton);
            container.Register<IPostsResource, PostsResource>(Lifestyle.Singleton);
            container.Register<IUsersResource, UsersResource>(Lifestyle.Singleton);
            container.Register<IPostLikesResource, PostLikesResource>(Lifestyle.Singleton);
            container.Register<IPostContentsResource, PostContentsResource>(Lifestyle.Singleton);
            container.Register<IMediaResource, MediaResource>(Lifestyle.Singleton);
            container.Register<IAlbumResource, AlbumResource>(Lifestyle.Singleton);
            container.Register<IEducationResource, EducationResource>(Lifestyle.Singleton);
            container.Register<IHobbyResource, HobbyResource>(Lifestyle.Singleton);
            container.Register<IAddressResource, AddressResource>(Lifestyle.Singleton);
            container.Register<ITagsResource, TagsResource>(Lifestyle.Singleton);
            container.Register<IViewCountResource, ViewCountResource>(Lifestyle.Singleton);
            container.Register<IChatMessagesResource, ChatMessagesResource>(Lifestyle.Singleton);

            // SI Helpers and Utilities registry
            container.Register<IImageHelper, ImageHelper>(Lifestyle.Singleton);
            container.Register<IErrorSignaler, ErrorSignaler>(Lifestyle.Singleton);
            container.Register<IHttpClientHelper, HttpClientHelper>(Lifestyle.Singleton);
            container.Register<IConfigurationHelper, ConfigurationHelper>(Lifestyle.Singleton);

            // SI Token Identity registry
            container.Register<IdentityDbContext<BlogUser>, BlogIdentityDbContext>(Lifestyle.Singleton);
            container.Register<IUserStore<BlogUser>, BlogUserStore>(Lifestyle.Singleton);
            container.Register<IBlogDbRepository, BlogDbRepository>(Lifestyle.Singleton);
            container.Register<BlogUserManager, BlogUserManager>(Lifestyle.Singleton);

            // SI Registrations
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            container.Verify();

            // Register the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new ApiRequestLoggerAttribute());
        }
    }
}