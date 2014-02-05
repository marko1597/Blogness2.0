using System.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Unauthorized()
        {
            Response.StatusCode = 403;
            return View("Unauthorized");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}
