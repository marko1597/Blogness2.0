using System;
using System.Text;
using System.Web.Http;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Web.Api.Models;
using Blog.Common.Web.Attributes;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class LogController : ApiController
    {
        private readonly IErrorSignaler _errorSignaler;

        public LogController(IErrorSignaler errorSignaler)
        {
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/log")]
        public ErrorLogModel Get()
        {
            try
            {
                return new ErrorLogModel();
            }
            catch
            {
                return new ErrorLogModel();
            }
        }

        [HttpPost]
        [Route("api/log")]
        public void Post(ErrorLogModel error)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine(error.ErrorUrl + Environment.NewLine);
                sb.AppendLine(error.ErrorMessage + Environment.NewLine);
                sb.AppendLine(error.StackTrace + Environment.NewLine);
                sb.AppendLine(error.Cause + Environment.NewLine);

                _errorSignaler.SignalFromCurrentContext(new Exception(sb.ToString()));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
