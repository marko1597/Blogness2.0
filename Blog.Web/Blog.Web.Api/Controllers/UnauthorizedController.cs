﻿using System.Web.Mvc;

namespace Blog.Web.Api.Controllers
{
    public class UnauthorizedController : Controller
    {
        public ActionResult Index()
        {
            Response.StatusCode = 403;
            var obj = new {Status = 401, Message = "unauthorized"};
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
	}
}