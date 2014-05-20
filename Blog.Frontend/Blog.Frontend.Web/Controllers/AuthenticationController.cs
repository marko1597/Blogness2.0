using System.Web.Mvc;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Web.Attributes;
using Blog.Backend.Common.Web.Authentication;
using Blog.Frontend.Web.Models;

namespace Blog.Frontend.Web.Controllers
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
            var login = new Backend.Common.Contracts.ViewModels.Login
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
            var login = new Backend.Common.Contracts.ViewModels.Login
            {
                Username = model.UserName
            };

            var result = AuthenticationApiFactory.GetInstance().Create().Logout(login);

            if (result)
            {
                _authentication.SignOut(new User { UserName = login.Username });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}