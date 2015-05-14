using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf;

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

                int? userIdProperty = GetUserIdProperty(model);

                if (userIdProperty != null && userIdProperty != 0)
                {
                    var username = actionContext.ControllerContext.RequestContext.Principal.Identity.Name;
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

        private static int? GetUserIdProperty(object src)
        {
            var properties = src.GetType().GetProperties();

            if (src.GetType() == typeof (User))
            {
                var userIdAsProperty = GetPropValue(src, "Id");
                return (int)userIdAsProperty;
            }

            foreach (var property in properties)
            {
                if (property.Name == "User" || property.Name == "Leader")
                {
                    var userPropName = property.Name;
                    var userValue = GetPropValue(src, userPropName);
                    if (userValue == null) return null;

                    var userIdFromObject = GetPropValue(userValue, "Id");
                    if (userIdFromObject == null) return null;
                    return (int)userIdFromObject;
                }

                if (property.Name == "UserId")
                {
                    var userIdAsProperty = GetPropValue(src, "UserId");
                    return (int) userIdAsProperty;
                }
                
                var propertyValue = GetPropValue(src, property.Name);
                if (propertyValue == null 
                    || propertyValue is string
                    || propertyValue is DateTime
                    || propertyValue.GetType().IsPrimitive
                    || propertyValue is IEnumerable
                    || propertyValue.GetType().IsArray) continue;

                var objectClass = GetPropValue(src, property.Name);
                var recursiveResult = GetUserIdProperty(objectClass);

                return recursiveResult;
            }

            return null;
        }

        private static object GetPropValue(object src, string propName)
        {
            var property = src.GetType().GetProperty(propName);
            return property == null ? null : property.GetValue(src, null);
        }
    }
}
