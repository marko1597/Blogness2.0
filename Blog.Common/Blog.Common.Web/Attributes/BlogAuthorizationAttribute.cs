using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogAuthorizationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        [Import]
        public ISession Session { get; set; }

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

            var session = Session.GetByUser(filterContext.HttpContext.User.Identity.Name);

            if (session != null)
            {
                return session.Error == null ? null : new HttpUnauthorizedResult();
            }
            return new HttpUnauthorizedResult();
        }
    }
}