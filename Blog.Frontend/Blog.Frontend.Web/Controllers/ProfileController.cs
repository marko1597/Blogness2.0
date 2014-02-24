using System.Web.Mvc;
using System.Threading.Tasks;
using Blog.Frontend.Web.Attributes;
using Blog.Frontend.Web.Models;

namespace Blog.Frontend.Web.Controllers
{
    [BlogAuth]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await new UserStore().FindByNameAsync
        //        if (user != null)
        //        {
        //            await SignInAsync(user, model.RememberMe);
        //            return RedirectToLocal(returnUrl);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid username or password.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //} 
    }
}