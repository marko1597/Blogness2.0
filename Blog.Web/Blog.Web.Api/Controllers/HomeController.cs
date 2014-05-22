using System.Web.Mvc;

namespace Blog.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Response.StatusCode = 403;
            return View();
        }
    }
}
