using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Admin.Web.Models.UserRolesAdmin;
using Blog.Common.Identity.Role;
using Blog.Common.Identity.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Admin.Web.Controllers
{
    public class UserRoleController : Controller
    {
        private BlogUserManager _userManager;
        public BlogUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<BlogUserManager>();
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

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            var users = new List<BlogUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();

            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid) return View();

            var role = new BlogRole(userRoleViewModel.Name)
            {
                Description = userRoleViewModel.Description
            };

            // Save the new Description property:
            var roleresult = await RoleManager.CreateAsync(role);
            if (roleresult.Succeeded) return RedirectToAction("Index");

            ModelState.AddModelError("", roleresult.Errors.First());
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var userRoleViewModel = new UserRoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            // Update the new Description property for the ViewModel:
            return View(userRoleViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id,Description")] UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid) return View();

            var role = await RoleManager.FindByIdAsync(userRoleViewModel.Id);
            role.Name = userRoleViewModel.Name;

            // Update the new Description property:
            role.Description = userRoleViewModel.Description;
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }

                IdentityResult result;

                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }

                if (result.Succeeded) return RedirectToAction("Index");
                ModelState.AddModelError("", result.Errors.First());

                return View();
            }
            return View();
        }
    }
}
