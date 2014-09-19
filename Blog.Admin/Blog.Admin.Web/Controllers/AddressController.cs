using System;
using System.Web;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Identity.Role;
using Blog.Common.Identity.User;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Admin.Web.Controllers
{
    public class AddressController : Controller
    {
        #region Members and Constructor

        private readonly IUsersResource _usersResource;

        private readonly IAddressResource _addressResource;

		private readonly IErrorSignaler _errorSignaler;

        public AddressController(IAddressResource addressResource, IUsersResource usersResource, 
            IErrorSignaler errorSignaler)
        {
            _addressResource = addressResource;
            _usersResource = usersResource;
			_errorSignaler = errorSignaler;
		}

		private BlogUserManager _userManager;
		public BlogUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().Get<BlogUserManager>();
			}
			set
			{
				_userManager = value;
			}
		}

		private BlogRoleManager _roleManager;
		public BlogRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? HttpContext.GetOwinContext().Get<BlogRoleManager>();
			}
			set
			{
				_roleManager = value;
			}
		}

		#endregion

        #region Index

        // GET: Users/1/Address
		public ActionResult Index(int userId)
		{
			try
			{
			    var user = _usersResource.Get(userId);
                if (user.Address == null) throw new Exception("Failed to get address list. Try refreshing the page.");

			    ViewBag.AddressHeader = string.Format("{0} {1}'s Address", user.FirstName, user.LastName);
			    ViewBag.Username = user.UserName;

                return View(user.Address);
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View();
			} 
		}
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Address address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(address);
                }

                var result = _addressResource.Update(address);
                if (result.Error != null) throw new Exception(result.Error.Message);

                return Redirect(string.Format("~/Users/Details/{0}", address.UserId));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = ex.Message;
                return View(address);
            }
        }

        #endregion
    }
}
