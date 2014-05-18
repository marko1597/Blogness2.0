using System;
using System.Web;
using System.Web.Http;
using Blog.Backend.Common.Web.Authentication;

namespace Blog.Backend.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogApiAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return base.IsAuthorized(actionContext);
            var session = ApiFactory.GetInstance().CreateApi().IsLoggedIn(HttpContext.Current.User.Identity.Name);
            return session != null;
        }
    }
}
