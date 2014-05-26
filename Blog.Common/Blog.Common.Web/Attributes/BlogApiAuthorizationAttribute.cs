using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Blog.Services.Implementation.Interfaces;
using WebApi.AuthenticationFilter;

namespace Blog.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogApiAuthorizationAttribute : AuthenticationFilterAttribute
    {
        [Import]
        public ISession Session { get; set; }

        public override void OnAuthentication(HttpAuthenticationContext context)
        {
            if (!Authenticate(context))
            {
                context.ErrorResult = new StatusCodeResult(HttpStatusCode.Unauthorized, context.Request);
            }
        }

        private bool Authenticate(HttpAuthenticationContext context)
        {
            var user = context.Principal.Identity;

            if (!user.IsAuthenticated)
            {
                return false;
            }

            var session = Session.GetByUser(user.Name);
            return session != null && session.Error == null;
        }
    }
}
