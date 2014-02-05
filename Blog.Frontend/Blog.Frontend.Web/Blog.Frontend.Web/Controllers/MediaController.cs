using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Frontend.Common;
using Blog.Frontend.Services;
using Lib.Web.Mvc;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using Blog.Frontend.Web.CustomHelpers.Authentication;
using Blog.Frontend.Web.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    public class MediaController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public MediaController(IBlogService service)
        {
            _service = service;
        }

        #endregion

        #region Private Members

        public string StorageRoot
        {
            get { return ConfigurationManager.AppSettings["StorageRoot"]; }
        }

        #endregion
        
        // TODO
        public ActionResult GetMedia(int mediaId)
        {
            var media = _service.GetUserMedia(mediaId);
            var file = new FileInfo(media.MediaPath);
            return new RangeFilePathResult(media.MediaType, file.FullName, file.LastWriteTimeUtc, file.Length);
        }

        // TODO
        public ActionResult GetAllMedia(int userId)
        {
            var lMedia = _service.GetAllUserMedia(userId);
            return View();
        }

        // TODO
        [CustomAuthorizationAttribute]
        public ActionResult DeleteMedia(int mediaId)
        {
            _service.DeleteMedia(mediaId);
            return PartialView("_PostModify", _service.GetAllUserMedia(mediaId));
        }

        public ActionResult Browse(string CKEditorFuncNum)
        {
            var lMedia = _service.GetAllUserMediaGrouped(UserTemp.UserId);

            var model = new MediaListView
            {
                Files = lMedia,
                CKEditorFuncNum = CKEditorFuncNum
            };

            return View(model);
        }

        [CustomAuthorizationAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        [JsonFilter(Param = "data", RootType = typeof(MediaUpload))]
        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var bytePostedFile = new byte[upload.ContentLength];
            upload.InputStream.Read(bytePostedFile, 0, upload.ContentLength);

            var media = new Media
                {
                    ThumbnailContent = null,
                    ThumbnailPath = Utils.GenerateImagePath(UserTemp.UserId, StorageRoot) + "tn\\" + Path.GetFileName(upload.FileName),
                    MediaContent = bytePostedFile,
                    MediaPath = Utils.GenerateImagePath(UserTemp.UserId, StorageRoot) + Path.GetFileName(upload.FileName),
                    MediaType = upload.ContentType,
                    FileName = upload.FileName,
                    ExternalUrl = "",
                    UserId = UserTemp.UserId,
                    CreatedBy = UserTemp.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = UserTemp.UserId,
                    ModifiedDate = DateTime.UtcNow
                };

            _service.AddMedia(media);

            return View();
        }
    }
}
