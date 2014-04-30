using System.Web.Mvc;
using Blog.Backend.Common.Web.Attributes;

namespace Blog.Frontend.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}