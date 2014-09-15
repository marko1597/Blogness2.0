using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Elmah;
using WebApi.AuthenticationFilter;

namespace Blog.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogApiAuthorizationAttribute : AuthenticationFilterAttribute
    {
        [Import]
        public IErrorSignaler ErrorSignaler { get; set; }

        public override void OnAuthentication(HttpAuthenticationContext context)
        {
            if (!Authenticate(context))
            {
                context.ErrorResult = new StatusCodeResult(HttpStatusCode.Unauthorized, context.Request);
            }
        }

        private bool Authenticate(HttpAuthenticationContext context)
        {
            try
            {
                var user = context.Principal.Identity;
                return user.IsAuthenticated;
            }
            catch (Exception ex)
            {
                ErrorSignaler.SignalFromCurrentContext(ex);
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
