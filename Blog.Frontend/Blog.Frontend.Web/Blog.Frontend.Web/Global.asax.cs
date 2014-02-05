using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Frontend.Services;
using Ninject;
using Blog.Frontend.Web.App_Start;
using Blog.Frontend.Web.CustomHelpers.Authentication;

namespace Blog.Frontend.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            SetupDependencyInjection();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new CustomViewEngine());
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            UserTemp.Username = UserTemp.Username ?? "";
            var newUser = new CustomPrincipal(UserTemp.Username)
                              {
                                  UserId = UserTemp.UserId,
                                  FirstName = UserTemp.FirstName,
                                  LastName = UserTemp.LastName
                              };

            HttpContext.Current.User = UserTemp.UserId == 0 ? new CustomPrincipal(string.Empty) : newUser;
        }

        protected void Application_AuthorizeRequest(Object sender, EventArgs e)
        {
            if (UserTemp.UserId > 0)
            {

            }
        }

        /*
        Application_BeginRequest
        Application_AuthenticateRequest
        Application_AuthorizeRequest
        Application_ResolveRequestCache
        Application_AcquireRequestState
        Application_PreRequestHandlerExecute
        Application_PreSendRequestHeaders
        Application_PreSendRequestContent
        Application_PostRequestHandlerExecute
        Application_ReleaseRequestState
        Application_UpdateRequestCache
        Application_EndRequest 
         * 
         * 
        Application_AcquireRequestState 
        Application_AuthenticateRequest 
        Application_AuthorizeRequest 
        Application_BeginRequest 
        Application_Disposed 
        Application_EndRequest 
        Application_Error 
        Application_PostAcquireRequestState 
        Application_PostAuthenticateRequest 
        Application_PostAuthorizeRequest 
        Application_PostMapRequestHandler 
        Application_PostReleaseRequestState 
        Application_PostRequestHandlerExecute 
        Application_PostResolveRequestCache 
        Application_PostUpdateRequestCache 
        Application_PreRequestHandlerExecute 
        Application_PreSendRequestContent 
        Application_PreSendRequestHeaders 
        Application_ReleaseRequestState 
        Application_ResolveRequestCache 
        Application_UpdateRequestCache 
        Application_Init 
        Application_Start 
        Application_End 
        */

        public void SetupDependencyInjection()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IBlogService>().To<BlogServiceApi>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}