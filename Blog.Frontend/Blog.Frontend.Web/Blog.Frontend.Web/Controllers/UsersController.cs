using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using System;
using System.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    public class UsersController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public UsersController(IBlogService service)
        {
            _service = service;
        }

        #endregion

        [CustomAuthorizationAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            var user = _service.GetUser(Convert.ToInt32(RouteData.Values["id"].ToString()), string.Empty);
            return View(user);
        }

    }
}
