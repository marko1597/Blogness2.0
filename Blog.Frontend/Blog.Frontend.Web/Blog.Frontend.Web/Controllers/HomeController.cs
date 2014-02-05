using System.Web.Mvc;
using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using Blog.Frontend.Web.Filters;
using Blog.Frontend.Web.Models;

namespace Blog.Frontend.Web.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public HomeController(IBlogService service)
        {
            _service = service;
        }

        #endregion

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            return View("PopularPosts", _service.GetPopularPosts(20));
        }

        public ActionResult Index()
        {
            return View("PopularPosts", _service.GetPopularPosts(20));
        }

        [CustomAuthorization]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        [CustomAuthorization]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
