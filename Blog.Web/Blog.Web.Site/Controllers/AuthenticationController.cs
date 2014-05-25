using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Web.Site.Models;

namespace Blog.Web.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationHelper _authentication;

        public AuthenticationController(IAuthenticationHelper authentication)
        {
            _authentication = authentication;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [AllowCrossSite]
        public JsonResult Login(LoginViewModel model, string returnUrl)
        {
            var login = new Common.Contracts.ViewModels.Login
            {
                Username = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var result = AuthenticationApiFactory.GetInstance().Create().Login(login);
            if (result.User != null && result.Session != null)
            {
                _authentication.SignIn(result.User);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowCrossSite]
        public JsonResult Logout(LoginViewModel model, string returnUrl)
        {
            var login = new Common.Contracts.ViewModels.Login
            {
                Username = model.UserName
            };

            var result = AuthenticationApiFactory.GetInstance().Create().Logout(login);

            if (result == null)
            {
                _authentication.SignOut(new User { UserName = login.Username });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}