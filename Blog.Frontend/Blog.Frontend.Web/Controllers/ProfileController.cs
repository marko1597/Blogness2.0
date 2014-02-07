using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}