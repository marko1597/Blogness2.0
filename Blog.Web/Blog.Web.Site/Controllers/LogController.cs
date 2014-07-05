using System;
using System.Web.Mvc;
using Blog.Common.Web.Extensions.Elmah;

namespace Blog.Web.Site.Controllers
{
    public class LogController : Controller
    {

        private readonly IErrorSignaler _errorSignaler;

        public LogController(IErrorSignaler errorSignaler)
        {
            _errorSignaler = errorSignaler;
        }

        public ActionResult Index()
        {
            return View();
        }

        public void Error(string message)
        {
            try
            {
                _errorSignaler.SignalFromCurrentContext(new Exception(message));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}