using System;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Admin.Web.Controllers
{
    public class HobbiesController : Controller
    {
        #region Members and Constructor

        private readonly IUsersResource _usersResource;

        private readonly IHobbyResource _hobbyResource;

		private readonly IErrorSignaler _errorSignaler;

        public HobbiesController(IHobbyResource hobbyResource, IUsersResource usersResource, 
            IErrorSignaler errorSignaler)
        {
            _hobbyResource = hobbyResource;
            _usersResource = usersResource;
			_errorSignaler = errorSignaler;
		}

		#endregion

        #region Index

        // GET: Users/1/Hobbies
		public ActionResult Index(int userId)
		{
			try
			{
			    var user = _usersResource.Get(userId);
                if (user.Hobbies == null) throw new Exception("Failed to get hobby list. Try refreshing the page.");

			    ViewBag.HobbiesHeader = string.Format("{0} {1}'s Hobbies", user.FirstName, user.LastName);
			    ViewBag.Username = user.UserName;
			    ViewBag.UserId = user.Id;

                return View(user.Hobbies);
			}
			catch (Exception ex)
			{
				_errorSignaler.SignalFromCurrentContext(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View();
			} 
		}
        
        #endregion

        // POST: Hobbies/Create
        [HttpPost]
        public ActionResult Create(Hobby hobby)
        {
            try
            {
                if (!ModelState.IsValid) return Json(ModelState);

                var result = _hobbyResource.Add(hobby);
                if (result.Error != null) throw new Exception(result.Error.Message);
                return Json(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return Json(hobby);
            }
        }

        // POST: Hobbies/Edit/5
        [HttpPost]
        public ActionResult Edit(Hobby hobby)
        {
            try
            {
                if (!ModelState.IsValid) return Json(ModelState);

                var result = _hobbyResource.Update(hobby);
                if (result.Error != null) throw new Exception(result.Error.Message);
                return Json(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return Json(hobby);
            }
        }

        // POST: Hobbies/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid) return Json(ModelState);

                var result = _hobbyResource.Delete(id);
                if (!result) throw new Exception("Failed to remove hobby.");

                return Json(true);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return Json(false);
            }
        }
    }
}
