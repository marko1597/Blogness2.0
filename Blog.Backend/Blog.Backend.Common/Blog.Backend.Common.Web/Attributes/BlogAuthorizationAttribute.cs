using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Blog.Backend.Common.Web.Authentication;

namespace Blog.Backend.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogAuthorizationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            filterContext.Result = GetCodeResult(filterContext);
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }

        private HttpStatusCodeResult GetCodeResult(AuthenticationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                return new HttpUnauthorizedResult();
            }

            var session = AuthenticationApiFactory.GetInstance().Create().IsLoggedIn(filterContext.Principal.Identity.Name);
            return session != null ? null : new HttpUnauthorizedResult() ;
        }
    }
}