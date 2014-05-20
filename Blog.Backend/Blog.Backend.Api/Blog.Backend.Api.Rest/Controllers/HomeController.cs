using System.Web.Mvc;

namespace Blog.Backend.Api.Rest.Controllers
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
