using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Blog.Common.Utils.Helpers.Elmah;

namespace Blog.Common.Web.Attributes
{
    [ExcludeFromCodeCoverage]
    public class ApiRequestLoggerAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();

        private IErrorSignaler _errorSignaler;
        public IErrorSignaler ErrorSignaler
        {
            get { return _errorSignaler ?? new ErrorSignaler(); }
            set { _errorSignaler = value; }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _stopWatch.Stop();
            var executionTime = _stopWatch.ElapsedMilliseconds;
            var log = new Exception(GetLogMessage(actionExecutedContext, executionTime));
            ErrorSignaler.SignalFromCurrentContext(log);
        }

        private string GetLogMessage(HttpActionExecutedContext actionExecutedContext, long duration)
        {
            var sb = new StringBuilder();
            sb.AppendLine("============================================================");
            sb.AppendLine(string.Format("From: {0}", actionExecutedContext.Request.Headers.From));
            sb.AppendLine(string.Format("Referrer: {0}", actionExecutedContext.Request.Headers.Referrer));
            sb.AppendLine(string.Format("Host: {0}", actionExecutedContext.Request.Headers.Host));
            sb.AppendLine(string.Format("User Agent: {0}", actionExecutedContext.Request.Headers.UserAgent));
            sb.AppendLine(string.Format("Date: {0}", actionExecutedContext.Request.Headers.Date));
            sb.AppendLine(string.Format("IsSuccess: {0}", actionExecutedContext.Response.IsSuccessStatusCode));
            sb.AppendLine(string.Format("Controller: {0}", actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName));
            sb.AppendLine(string.Format("Route: {0}", GetActionRoute(actionExecutedContext.ActionContext.ControllerContext)));
            sb.AppendLine(string.Format("Request Duration: {0} milliseconds", duration));
            sb.AppendLine("============================================================");

            return sb.ToString();
        }

        private static string GetActionRoute(HttpControllerContext controllerContext)
        {
            try
            {
                var routeName = string.Empty;

                foreach (var r in controllerContext.RouteData.Route.DataTokens
                    .Where(r => r.Key == "actions")
                    .Where(r => r.Value != null || r.GetType() == typeof (HttpActionDescriptor[])))
                {
                    routeName = ((HttpActionDescriptor[])r.Value)[0].ActionName;
                    break;
                }

                return routeName;
            }
            catch (Exception)
            {
                return "Cannot get action name";
            }
        }
    }
}
