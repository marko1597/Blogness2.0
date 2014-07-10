using System;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Web.Site.Models;

namespace Blog.Web.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISessionResource _session;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IErrorSignaler _errorSignaler;

        public AuthenticationController(ISessionResource session, IAuthenticationHelper authenticationHelper, IErrorSignaler errorSignaler)
        {
            _session = session;
            _authenticationHelper = authenticationHelper;
            _errorSignaler = errorSignaler;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [AllowCrossSite]
        public JsonResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var ip = Request.ServerVariables["REMOTE_ADDR"];
                var result = _session.Login(model.UserName, model.Password, ip);

                if (result.User != null && result.Session != null)
                {
                    _authenticationHelper.SignIn(result.User);
                    _errorSignaler.SignalFromCurrentContext(new Exception(
                        string.Format("User {0} logged in", model.UserName)));
                }
                else
                {
                    _errorSignaler.SignalFromCurrentContext(new Exception(
                        string.Format("Invalid login attempt: {0}/{1}", model.UserName, model.Password)));
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return null;
        }

        [AllowCrossSite]
        public JsonResult Logout(LoginViewModel model, string returnUrl)
        {
            try
            {
                var result = _session.Logout(model.UserName);

                if (result == null)
                {
                    _authenticationHelper.SignOut(new User {UserName = model.UserName});
                    _errorSignaler.SignalFromCurrentContext(new Exception(
                        string.Format("User {0} logged off", model.UserName)));
                }
                else
                {
                    _errorSignaler.SignalFromCurrentContext(new Exception(
                       string.Format("Could not logout {0}", model.UserName)));
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return null;
        }
    }
}