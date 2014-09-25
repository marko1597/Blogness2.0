﻿using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
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

                int? userIdProperty = GetUserIdProperty(model);

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

        private static int? GetUserIdProperty(object src)
        {
            var properties = src.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "User")
                {
                    var userValue = GetPropValue(src, "User");
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
                    ||propertyValue is string
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
