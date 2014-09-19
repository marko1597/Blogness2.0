using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Admin.Web.Helpers;
using Blog.Admin.Web.Models.Users;
using Blog.Common.Contracts;
using Blog.Common.Identity.Role;
using Blog.Common.Identity.User;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Admin.Web.Controllers
{
	public class UsersController : Controller
	{
		#region Members and Constructor

		private readonly IUsersResource _usersResource;

		private readonly IUserHelper _userHelper;

		private readonly IConfigurationHelper _configurationHelper;

		private readonly IErrorSignaler _errorSignaler;

		public UsersController(IUsersResource usersResource, IUserHelper userHelper, 
			IConfigurationHelper configurationHelper, IErrorSignaler errorSignaler)
		{
			_usersResource = usersResource;
			_userHelper = userHelper;
			_configurationHelper = configurationHelper;
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

        // GET: Users
		public async Task<ActionResult> Index()
		{
			try
			{
				var users = _usersResource.GetUsers(10, 0);
				var userViewModels = new List<UserViewModel>();

				foreach (var u in users)
				{
					var role = string.IsNullOrEmpty(u.IdentityId) ? string.Empty : await GetRoles(u.IdentityId);

					var userViewModel = new UserViewModel
					{
						Id = u.Id,
						UserName = u.UserName,
						FirstName = u.FirstName,
						LastName = u.LastName,
						IdentityId = u.IdentityId,
						Role = role
					};

					userViewModels.Add(userViewModel);
				}

				return View(userViewModels);
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = "Failed to get users list. Try refreshing the page.";
				return View();
			} 
		}

        #endregion

        #region Details

        // GET: Users/Details/5
		public async Task<ActionResult> Details(int id)
		{
			try
			{
				var user = await GetUserViewModel(id);

				if (user != null && user.Error !=null) 
					throw new Exception(user.Error.Message);

				return View(user);
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View();
			}
		}

        #endregion

        #region Create

        // GET: Users/Create
		public ActionResult Create()
		{
			try
			{
				return View(GetDefaultCreateUserModel());
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				TempData.Add("ErrorMessage", ex.Message);
				return RedirectToAction("Index");
			}
		}

        // POST: Users/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateUserViewModel model)
		{
			try
			{
				if (!ModelState.IsValid) return View(GetDefaultCreateUserModel());

				var user = new BlogUser { UserName = model.Username, Email = model.Email };
				var result = await UserManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					var roleResult = await UserManager.AddToRolesAsync(user.Id, new[] { model.SelectedRole });
					if (!roleResult.Succeeded)
					{
						AddErrors(roleResult);
						return View(GetDefaultCreateUserModel());
					}

					var bypassCreateProfile = Convert.ToBoolean(_configurationHelper.GetAppSettings("ByPassProfileCreation"));
					if (bypassCreateProfile) return RedirectToAction("Index", "Users");

					var blogUser = await _userHelper.AddBlogUser(model);
					if (blogUser.Error == null) return RedirectToAction("Index", "Users");

					ViewBag.UserCreationError = blogUser.Error.Message;
					return View(model);
				}
				AddErrors(result);
				return View(GetDefaultCreateUserModel());
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View(GetDefaultCreateUserModel());
			}
        }

        #endregion

        #region Edit
        // GET: Users/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			try
			{
			    var userViewModel = await GetUserViewModel(id);
			    var createUserViewModel = new CreateUserViewModel
			                              {
                                              Id = userViewModel.Id,
                                              Username = userViewModel.UserName,
                                              FirstName = userViewModel.FirstName,
                                              LastName = userViewModel.LastName,
                                              Email = userViewModel.EmailAddress,
                                              BirthDate = userViewModel.BirthDate,
                                              SelectedRole = userViewModel.Role,
                                              IdentityId = userViewModel.IdentityId,
                                              RolesAvailable = GetUserRolesAvailable()
			                              };
                return View(createUserViewModel);
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
			    ViewBag.ErrorMessage = ex.Message;
				return View();
			}
		}

		// POST: Users/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(CreateUserViewModel model)
		{
            try{
			    if (!ModelState.IsValid) return View(GetDefaultCreateUserModel());

				var user = new BlogUser { UserName = model.Username, Email = model.Email };
				var result = await UserManager.UpdateAsync(user);

				if (result.Succeeded)
				{
				    var roles = RoleManager.Roles.Select(a => a.Name).ToArray();
                    var roleRemoveResult = await UserManager.RemoveFromRolesAsync(user.Id, roles);
                    if (!roleRemoveResult.Succeeded) throw new Exception("Failed to update this user's role.");

				    var addToRoleResult = await UserManager.AddToRoleAsync(user.Id, model.SelectedRole);
                    if (!addToRoleResult.Succeeded)
					{
                        AddErrors(addToRoleResult);
						return View(GetDefaultCreateUserModel());
					}

				    var userProfile = new User
				                      {
				                          UserName = model.Username,
				                          FirstName = model.FirstName,
				                          LastName = model.LastName,
				                          BirthDate = model.BirthDate,
				                          IdentityId = user.Id,
				                          EmailAddress = model.Email
				                      };

                    var blogUser = _usersResource.Update(userProfile);
					if (blogUser.Error == null) return RedirectToAction("Index", "Users");

					ViewBag.UserCreationError = blogUser.Error.Message;
					return View(model);
				}
				AddErrors(result);
				return View(GetDefaultCreateUserModel());
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View(GetDefaultCreateUserModel());
			}
		}

        #endregion

        #region Delete
        // GET: Users/Delete/5
		public async Task<ActionResult> Delete(string id)
		{
			try
			{

				if (string.IsNullOrEmpty(id)) throw new Exception("Empty parameter on Users/Delete");

				var user = await UserManager.FindByIdAsync(id);
				if (user != null) return View(user);

				throw new Exception("Failed to get user on Users/Delete");
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				TempData.Add("ErrorMessage", ex.Message);
				return RedirectToAction("Index");
			}
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteUser(string id)
		{
			try
			{
				if (!ModelState.IsValid) return RedirectToAction("Index");

				if (string.IsNullOrEmpty(id)) throw new Exception("Empty parameter on Users/Delete");

				var user = await UserManager.FindByIdAsync(id);
				if (user == null) throw new Exception("Failed to get user on Users/Delete");
				
				var result = await UserManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					var deleteUser = _userHelper.DeleteUser(user.UserName);
					if (deleteUser.Error != null) return RedirectToAction("Index");

					TempData.Add("ErrorMessage", "Identity has been deleted but profile failed to be updated.");
					RedirectToAction("Index");
				}

				ModelState.AddModelError("", result.Errors.First());
				return View();
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View();
			}
		}

        #endregion

        #region Helpers
        private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		private async Task<string> GetRoles(string id)
		{
			try
			{
				var roles = await UserManager.GetRolesAsync(id);
				return roles != null ? roles.ToList().FirstOrDefault() : string.Empty;
			}
			catch
			{
				return string.Empty;
			}
		}

	    private List<CreateUserRolesAvailable> GetUserRolesAvailable()
	    {
            var roles = RoleManager.Roles;
            var availableRoles = roles.Select(role => new CreateUserRolesAvailable
            {
                IsSelected = false,
                RoleName = role.Name
            }).ToList();

	        return availableRoles;
	    }

		private CreateUserViewModel GetDefaultCreateUserModel()
		{
			var model = new CreateUserViewModel
			{
				Username = string.Empty,
				Password = string.Empty,
				ConfirmPassword = string.Empty,
				Email = string.Empty,
				FirstName = string.Empty,
				LastName = string.Empty,
				BirthDate = DateTime.Now,
				SelectedRole = string.Empty,
                RolesAvailable = GetUserRolesAvailable()
			};

			return model;
		}

		private async Task<UserViewModel> GetUserViewModel(int id)
		{
			var user = _usersResource.Get(id);

			if (user.Error != null)
			{
				return new UserViewModel { Error = user.Error };
			}

			var role = string.IsNullOrEmpty(user.IdentityId) ? string.Empty : await GetRoles(user.IdentityId);
			var userViewModel = new UserViewModel
			{
                Id = id,
				UserName = user.UserName,
				EmailAddress = user.EmailAddress,
				FirstName = user.FirstName,
				LastName = user.LastName,
				IdentityId = user.IdentityId,
				BirthDate = user.BirthDate,
                Address = user.Address,
                Education = user.Education,
                Picture = user.Picture,
                Background = user.Background,
                Hobbies = user.Hobbies,
				Role = role
			};

			return userViewModel;
        }

        #endregion
    }
}
