using System.Web;
using System.Web.Mvc;
using Blog.Frontend.Common.Authentication;
using Blog.Frontend.Web.Attributes;
using Blog.Frontend.Web.Helpers;
using Blog.Frontend.Web.Models;

namespace Blog.Frontend.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthentication _authentication;

        public ProfileController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [BlogAuth]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                return Redirect("/blog");
            }
            return View();
        }

        public JsonResult Authenticate(LoginViewModel model, string returnUrl)
        {
            var login = new Backend.Common.Contracts.ViewModels.Login
            {
                Username = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var result = ApiFactory.GetInstance().CreateApi().Login(login);
            if (result.User != null && result.Session != null)
            {
                _authentication.SignIn(result.User);
            }

            return Json(result);
        }
    }
}