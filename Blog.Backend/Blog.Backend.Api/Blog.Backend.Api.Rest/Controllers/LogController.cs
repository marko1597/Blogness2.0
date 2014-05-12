using System;
using System.Text;
using System.Web.Http;
using Blog.Backend.Api.Rest.Models;
using Blog.Backend.Common.Web.Attributes;

namespace Blog.Backend.Api.Rest.Controllers
{
    [AllowCrossSiteApi]
    public class LogController : ApiController
    {
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

                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(sb.ToString()));
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
