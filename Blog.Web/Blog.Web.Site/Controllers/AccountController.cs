using System.Web.Mvc;

namespace Blog.Web.Site.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}