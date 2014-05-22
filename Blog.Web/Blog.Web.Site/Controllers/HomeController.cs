using System.Web.Mvc;
using Blog.Common.Web.Attributes;

namespace Blog.Web.Site.Controllers
{
    public class HomeController : Controller
    {
        [BlogAuthorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}