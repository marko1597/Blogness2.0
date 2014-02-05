using System;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace Blog.Frontend.Web.CustomHelpers.Attributes
{
    public class JsonFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type RootType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains("application/json")) return;
            filterContext.ActionParameters[Param] = new DataContractJsonSerializer(RootType).ReadObject(filterContext.HttpContext.Request.InputStream);
        }
    }
}