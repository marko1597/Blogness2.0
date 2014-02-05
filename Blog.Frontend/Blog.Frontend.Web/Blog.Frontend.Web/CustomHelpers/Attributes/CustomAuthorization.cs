using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Authentication;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Frontend.Web.CustomHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            IBlogService api = new BlogServiceApi();
            var session = api.IsLoggedIn(UserTemp.UserId);

            if (session != null)
            {
                if (session.Token != null)
                {
                    if (Common.Utils.IsGuid(session.Token))
                    {
                        return base.AuthorizeCore(httpContext);
                    }
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}