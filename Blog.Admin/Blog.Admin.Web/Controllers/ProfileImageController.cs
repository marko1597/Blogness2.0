using System;
using System.Web.Mvc;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Admin.Web.Controllers
{
    public class ProfileImageController : Controller
    {
        #region Members and Constructor

        private readonly IUsersResource _usersResource;

        private readonly IErrorSignaler _errorSignaler;

        public ProfileImageController(IUsersResource usersResource, IErrorSignaler errorSignaler)
        {
            _usersResource = usersResource;
            _errorSignaler = errorSignaler;
        }

        #endregion

        #region Index

        // GET: Users/{userId}/ProfileImage
        public ActionResult Index(int userId)
        {
            try
            {
                var user = _usersResource.Get(userId);
                return View(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = "Failed to get user images. Try refreshing the page.";
                return View();
            }
        }

        #endregion
    }
}