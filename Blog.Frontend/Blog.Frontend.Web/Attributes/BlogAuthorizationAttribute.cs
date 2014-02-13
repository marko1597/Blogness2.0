using System;
using System.Web.Mvc;
using Blog.Frontend.Common.Authentication;
using System.Web.Mvc.Filters;

namespace Blog.Frontend.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogAuthAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["username"] == null)
            {
                var ip = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
                var sessionByIp = ApiFactory.GetInstance().CreateApi().GetByIp(ip);

                if (sessionByIp != null && string.IsNullOrEmpty(sessionByIp.Token))
                {
                    return;
                }

                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var session = ApiFactory.GetInstance().CreateApi().IsLoggedIn(filterContext.HttpContext.Request.Cookies["username"].Value);
            if (session == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (string.IsNullOrEmpty(session.Token) || session.TimeValidity <= DateTime.Now)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["username"] == null)
            {
                var ip = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
                var sessionByIp = ApiFactory.GetInstance().CreateApi().GetByIp(ip);

                if (sessionByIp != null && string.IsNullOrEmpty(sessionByIp.Token))
                {
                    return;
                }

                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var session = ApiFactory.GetInstance().CreateApi().IsLoggedIn(filterContext.HttpContext.Request.Cookies["username"].Value);
            if (session == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (string.IsNullOrEmpty(session.Token) || session.TimeValidity <= DateTime.Now)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}