using System;
using System.ComponentModel.Composition;
using System.Web.Http;
using System.Web.Http.Controllers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogApiAuthorizationAttribute : AuthorizeAttribute
    {
        [Import]
        public ISession Session { get; set; }
        
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var user = ((ApiController)actionContext.ControllerContext.Controller).User.Identity;

            if (!user.IsAuthenticated) return base.IsAuthorized(actionContext);
            var session = Session.GetByUser(user.Name);
            return session != null;
        }
    }
}
