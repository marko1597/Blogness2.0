﻿using System.Web.Mvc;

namespace Blog.Admin.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}