using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Blog.Admin.Web.Models;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Admin.Web.Controllers
{
    public class ProfileImageController : Controller
    {
        #region Members and Constructor

        private readonly IUsersResource _usersResource;

        private readonly IMediaResource _mediaResource;

        private readonly IErrorSignaler _errorSignaler;

        private readonly IConfigurationHelper _configurationHelper;

        public ProfileImageController(IUsersResource usersResource, IMediaResource mediaResource,
            IErrorSignaler errorSignaler, IConfigurationHelper configurationHelper)
        {
            _usersResource = usersResource;
            _mediaResource = mediaResource;
            _errorSignaler = errorSignaler;
            _configurationHelper = configurationHelper;
        }

        #endregion

        #region Index

        // GET: Users/{userId}/ProfileImage
        public ActionResult ProfileImage(int userId)
        {
            try
            {
                var model = GetViewModel(userId, false);

                ViewBag.ViewHeader = string.Format("Change {0}'s profile picture", model.Username);
                ViewBag.Title = string.Format("{0}'s profile picture", model.Username);

                return View("ProfileImage", model);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = "Failed to get user image. Try refreshing the page.";
                return View("ProfileImage");
            }
        }

        // GET: Users/{userId}/BackgroundImage
        public ActionResult BackgroundImage(int userId)
        {
            try
            {
                var model = GetViewModel(userId, true);

                ViewBag.ViewHeader = string.Format("Change {0}'s background picture", model.Username);
                ViewBag.Title = string.Format("{0}'s background picture", model.Username);

                return View("ProfileImage", model);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = "Failed to get user image. Try refreshing the page.";
                return View("ProfileImage");
            }
        }

        #endregion

        #region Edit

        // POST: ProfileImage/Edit
        [HttpPost]
        public ActionResult Edit(ImageUploadViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View("ProfileImage", viewModel);

                SaveUploadedFile(viewModel.ImageUpload, viewModel.UserId, false);

                return RedirectToAction("Details", "Users", new { id = viewModel.UserId });
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = "Failed to get user image. Try refreshing the page.";
                return View("ProfileImage");
            }
        }

        // GET: ProfileImage/EditBackground
        [HttpPost]
        public ActionResult EditBackground(ImageUploadViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View("ProfileImage", viewModel);

                SaveUploadedFile(viewModel.ImageUpload, viewModel.UserId, true);

                return RedirectToAction("Details", "Users", new { id = viewModel.UserId});
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                ViewBag.ErrorMessage = "Failed to get user image. Try refreshing the page.";
                return View("ProfileImage");
            }
        }

        #endregion

        #region Helpers

        private ImageUploadViewModel GetViewModel(int userId, bool isBackground)
        {
            var user = _usersResource.Get(userId);
            var model = new ImageUploadViewModel
                        {
                            UserId = userId,
                            Username = user.UserName,
                            IsBackground = isBackground,
                            MediaUrl = isBackground ? user.Background.MediaUrl : user.Picture.MediaUrl
                        };

            return model;
        }

        private void SaveUploadedFile(HttpPostedFileBase httpPostedFileBase, int userId, bool isBackground)
        {
            if (httpPostedFileBase == null) throw new Exception("Image uploaded is missing!");

            var filename = Path.GetFileName(httpPostedFileBase.FileName);
            if (filename == null) throw new Exception("Image uploaded is missing!");

            var guid = Guid.NewGuid().ToString();
            filename = string.Format("{0}{1}", guid, Path.GetExtension(filename));

            var user = _usersResource.Get(userId);
            if (user == null) throw new Exception("User not found");
            if (user.Error != null) throw new Exception(user.Error.Message);

            var basepath = _configurationHelper.GetAppSettings("MediaLocation");
            var fullpath = Path.Combine(basepath, filename);
            httpPostedFileBase.SaveAs(fullpath);

            var album = isBackground ? "Background" : "Profile";

            var resultMedia = _mediaResource.AddAsContent(user, album, filename, fullpath,
                httpPostedFileBase.ContentType);

            if (!isBackground)
            {
                user.Picture = resultMedia;
            }
            else
            {
                user.Background = resultMedia;
            }

            var userResult = _usersResource.Update(user);
            if (userResult.Error != null)
            {
                throw new Exception(userResult.Error.Message);
            }
        }

        #endregion
    }
}