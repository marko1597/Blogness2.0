using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Blog.Common.Utils.Extensions;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogAuthorizationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        [Import]
        public ISessionResource Session { get; set; }

        [Import]
        public IErrorSignaler ErrorSignaler { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            try
            {
                filterContext.Result = GetCodeResult(filterContext);
            }
            catch (Exception ex)
            {
                ErrorSignaler.SignalFromCurrentContext(ex);
                throw new BlogException(ex.Message, ex.InnerException);
            }
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