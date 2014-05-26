using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Services.Implementation.Interfaces;
using Blog.Web.Site.Models;

namespace Blog.Web.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISession _session;
        private readonly IAuthenticationHelper _authenticationHelper;

        public AuthenticationController(ISession session, IAuthenticationHelper authenticationHelper)
        {
            _session = session;
            _authenticationHelper = authenticationHelper;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [AllowCrossSite]
        public JsonResult Login(LoginViewModel model, string returnUrl)
        {
            var ip = Request.ServerVariables["REMOTE_ADDR"];
            var result = _session.Login(model.UserName, model.Password, ip);

            if (result.User != null && result.Session != null)
            {
                _authenticationHelper.SignIn(result.User);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowCrossSite]
        public JsonResult Logout(LoginViewModel model, string returnUrl)
        {
            var result = _session.Logout(model.UserName);

            if (result == null)
            {
                _authenticationHelper.SignOut(new User { UserName = model.UserName });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}