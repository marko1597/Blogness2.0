using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Frontend.Common.Helper;
using Blog.Frontend.Common.Authentication;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
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
                var ip = filterContext.HttpContext.Request.UserHostAddress;
                var sessionByIp = ApiFactory.GetInstance().CreateApi().GetByIp(ip);

                if (sessionByIp != null && string.IsNullOrEmpty(sessionByIp.Token))
                {
                    return;
                }

                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var session = ApiFactory.GetInstance().CreateApi().IsLoggedIn(filterContext.HttpContext.Request.Cookies["username"].Value.ToString());
            if (session == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (string.IsNullOrEmpty(session.Token) || session.TimeValidity >= DateTime.Now)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["username"] == null)
            {
                var ip = filterContext.HttpContext.Request.UserHostAddress;
                var sessionByIp = ApiFactory.GetInstance().CreateApi().GetByIp(ip);

                if (sessionByIp != null && string.IsNullOrEmpty(sessionByIp.Token))
                {
                    return;
                }

                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var session = ApiFactory.GetInstance().CreateApi().IsLoggedIn(filterContext.HttpContext.Request.Cookies["username"].Value.ToString());
            if (session == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (string.IsNullOrEmpty(session.Token) || session.TimeValidity >= DateTime.Now)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}