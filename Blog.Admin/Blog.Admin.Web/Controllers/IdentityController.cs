using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Blog.Admin.Web.Models.Identity;
using Blog.Common.Identity.Models;
using Blog.Common.Identity.Role;
using Blog.Common.Identity.User;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Admin.Web.Controllers
{
    public class IdentityController : Controller
    {
		#region Members and Constructor

		private readonly IUsersResource _usersResource;
        
		private readonly IErrorSignaler _errorSignaler;

        public IdentityController(IUsersResource usersResource, IErrorSignaler errorSignaler)
		{
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

        // GET: Identity
		public ActionResult Index()
		{
			try
			{
				var users = _usersResource.GetUsersWithNoIdentityId();
                return View(users);
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = "Failed to get users list. Try refreshing the page.";
				return View();
			} 
		}

        #endregion

        #region Map

        // GET: Identity/Map/5
        public ActionResult Map(int id)
        {
            try
            {
                var model = GetMapIdentityViewModel(id);
                if (model.User == null) throw new Exception(string.Format("No user found with Id {0}", id));
                if (model.User.Error != null) throw new Exception(model.User.Error.Message);
                if (model.BlogUsers == null || model.BlogUsers.Count == 0) 
                    throw new Exception("No identities found. Try refreshing the page.");

                return View(model);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
            
        }

        // POST: Identity/Map/5
        [HttpPost]
        public ActionResult Map(MapIdentityViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.BlogUsers = model.BlogUsers ?? GetBlogUsers();
                    return View(model);
                }

                var user = model.User;
                user.IdentityId = model.SelectedIdentityId;

                _usersResource.Update(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = ex.Message;

                model.BlogUsers = model.BlogUsers ?? GetBlogUsers();
                return View(model);
            }
        }

        #endregion

        private List<BlogUser> GetBlogUsers()
        {
            var unusedIdentities = new List<BlogUser>();
            var blogUsersList = UserManager.GetBlogUsers();

            foreach (var blogUser in blogUsersList)
            {
                var userProfile = _usersResource.GetByIdentityId(blogUser.Id);

                if (userProfile == null ||
                    (userProfile.Error != null && userProfile.Error.Id == (int) Constants.Error.RecordNotFound))
                {
                    unusedIdentities.Add(blogUser);
                }
            }

            return unusedIdentities;
        }

        private MapIdentityViewModel GetMapIdentityViewModel(int id)
        {
            var model = new MapIdentityViewModel
            {
                User = _usersResource.Get(id),
                BlogUsers = GetBlogUsers()
            };

            return model;
        }
    }
}
