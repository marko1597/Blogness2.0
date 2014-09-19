using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Admin.Web.Helpers;
using Blog.Admin.Web.Models.Users;
using Blog.Common.Identity.Role;
using Blog.Common.Identity.User;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Admin.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersResource _usersResource;

        private readonly IUserHelper _userHelper;

        public UsersController(IUsersResource usersResource, IUserHelper userHelper)
        {
            _usersResource = usersResource;
            _userHelper = userHelper;
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

        // GET: Users
        public async Task<ActionResult> Index()
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

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var user = await GetUserViewModel(id);
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View(GetDefaultCreateUserModel());
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

                    var blogUser = await _userHelper.AddBlogUser(model);
                    if (blogUser.Error == null) return RedirectToAction("Index", "Users");

                    ViewBag.UserCreationError = blogUser.Error.Message;
                    return View(model);
                }
                AddErrors(result);
                return View(GetDefaultCreateUserModel());
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Errors.First());

            return View();
        }

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

        private CreateUserViewModel GetDefaultCreateUserModel()
        {
            var roles = RoleManager.Roles;
            var availableRoles = roles.Select(role => new CreateUserRolesAvailable
            {
                IsSelected = false,
                RoleName = role.Name
            }).ToList();

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
                RolesAvailable = availableRoles
            };

            return model;
        }

        private async Task<UserViewModel> GetUserViewModel(int id)
        {
            var user = _usersResource.Get(id);
            var role = string.IsNullOrEmpty(user.IdentityId) ? string.Empty : await GetRoles(user.IdentityId);

            var userViewModel = new UserViewModel
            {
                UserName = user.UserName,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IdentityId = user.IdentityId,
                BirthDate = user.BirthDate,
                Role = role
            };

            return userViewModel;
        }
    }
}
