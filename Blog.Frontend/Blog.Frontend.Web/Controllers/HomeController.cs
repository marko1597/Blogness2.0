using System.Web.Mvc;
using Blog.Frontend.Web.Attributes;

namespace Blog.Frontend.Web.Controllers
{
    public class HomeController : Controller
    {
        [BlogAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}