using Blog.Frontend.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    public class HomeController : Controller
    {
        [BlogAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}