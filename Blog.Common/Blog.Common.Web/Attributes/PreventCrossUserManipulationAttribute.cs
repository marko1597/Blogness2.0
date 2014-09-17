using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Wcf;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Common.Web.Attributes
{
    public class PreventCrossUserManipulationAttribute : ActionFilterAttribute
    {
        private IUsersResource _usersResource;
        public IUsersResource UsersResource
        {
            get
            {
                return _usersResource ?? new UsersResource();
            }
            set { _usersResource = value; }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var name = actionContext.ActionArguments.Keys.FirstOrDefault();
            if (name != null)
            {
                var model = actionContext.ActionArguments[name];
                if (model == null) throw new HttpResponseException(HttpStatusCode.InternalServerError);

                int? userIdProperty;

                if (model.GetType() == typeof (User))
                {
                    userIdProperty = (int?) GetPropValue(model, "Id");
                }
                else
                {
                    userIdProperty = GetIdFromUserProperty(model);
                    if (userIdProperty == null || userIdProperty == 0)
                    {
                        userIdProperty = (int?)GetPropValue(model, "UserId");
                    }
                }

                if (userIdProperty != null && userIdProperty != 0)
                {
                    var username = actionContext.RequestContext.Principal.Identity.Name;
                    if (string.IsNullOrEmpty(username))
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);

                    var user = UsersResource.GetByUserName(username);
                    if (user == null) throw new HttpResponseException(HttpStatusCode.InternalServerError);

                    if (userIdProperty != user.Id) throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            base.OnActionExecuting(actionContext);
        }

        private static int? GetIdFromUserProperty(object src)
        {
            var property = src.GetType().GetProperty("User");
            if (property == null) return null;

            var user = property.GetValue(src, null);
            if (user == null) return null;

            var userId = GetPropValue(user, "Id");
            return (int)userId;
        }

        private static object GetPropValue(object src, string propName)
        {
            var property = src.GetType().GetProperty(propName);
            return property == null ? null : property.GetValue(src, null);
        }
    }
}
