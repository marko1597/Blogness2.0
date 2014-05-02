using System.Web.Mvc;
using Blog.Backend.Common.Web.Attributes;

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