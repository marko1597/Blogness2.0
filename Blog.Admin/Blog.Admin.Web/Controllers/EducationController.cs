using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Admin.Web.Controllers
{
    public class EducationController : Controller
    {
        #region Members and Constructor

        private readonly IUsersResource _usersResource;

        private readonly IEducationResource _educationResource;

        private readonly IErrorSignaler _errorSignaler;

        public EducationController(IEducationResource educationResource, IUsersResource usersResource, IErrorSignaler errorSignaler)
        {
            _educationResource = educationResource;
            _usersResource = usersResource;
            _errorSignaler = errorSignaler;
        }

        #endregion

        #region Index

        // GET: Users/1/Education
        public ActionResult Index(int userId)
        {
            try
            {
                var user = _usersResource.Get(userId);
                if (user.Education == null) throw new Exception("Failed to get education list. Try refreshing the page.");

                ViewBag.EducationHeader = string.Format("{0} {1}'s Education", user.FirstName, user.LastName);
                ViewBag.Username = user.UserName;
                ViewBag.UserId = user.Id;

                return View(user.Education);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        #endregion

        // POST: Education/Create
        [HttpPost]
        public ActionResult Create(Education education)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(GetErrorList(ModelState));
                }

                var result = _educationResource.Add(education);
                if (result.Error != null) throw new Exception(result.Error.Message);
                return Json(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                _errorSignaler.SignalFromCurrentContext(ex);
                return Json(education);
            }
        }

        // POST: Education/Edit/5
        [HttpPost]
        public ActionResult Edit(Education education)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(GetErrorList(ModelState));
                }

                var result = _educationResource.Update(education);
                if (result.Error != null) throw new Exception(result.Error.Message);
                return Json(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                _errorSignaler.SignalFromCurrentContext(ex);
                return Json(education);
            }
        }

        // POST: Education/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(GetErrorList(ModelState));
                }

                var result = _educationResource.Delete(id);
                if (!result) throw new Exception("Failed to remove education.");

                return Json(true);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                _errorSignaler.SignalFromCurrentContext(ex);
                return Json(false);
            }
        }

        private Dictionary<string, string[]> GetErrorList(IEnumerable<KeyValuePair<string, ModelState>> modelState)
        {
            var errorList = modelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
            Response.StatusCode = 400;
            return errorList;
        }
    }
}