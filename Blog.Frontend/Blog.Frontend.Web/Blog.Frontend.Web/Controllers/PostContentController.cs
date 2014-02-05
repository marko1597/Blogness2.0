using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Frontend.Common;
using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using Blog.Frontend.Web.CustomHelpers.Authentication;
using Blog.Frontend.Web.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Lib.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    public class PostContentController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public PostContentController(IBlogService service)
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

        public ActionResult GetPostContent(int postContentId)
        {
            var content = _service.GetPostContent(postContentId);
            var file = new FileInfo(content.Media.MediaPath);
            return new RangeFilePathResult(content.Media.MediaType, file.FullName, file.LastWriteTimeUtc, file.Length);
        }

        [CustomAuthorizationAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        [JsonFilter(Param = "data", RootType = typeof(PostContentModify))]
        public ActionResult DeletePostContent(PostContentModify postContentModify)
        {
            _service.DeletePostContent(postContentModify.PostContentId);
            return PartialView("_PostModify", new PostView { Post = _service.GetPost(postContentModify.PostId), IsAdd = false, IsEdit = true, DisplayComments = false });
        }

        [CustomAuthorizationAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult AddPostContent(PostContentUpload postContentUpload)
        {
            var bytePostedFile = new byte[postContentUpload.PostContentUploadFile.ContentLength];
            postContentUpload.PostContentUploadFile.InputStream.Read(bytePostedFile, 0, postContentUpload.PostContentUploadFile.ContentLength);

            var mediaPath = Utils.GenerateImagePath(UserTemp.UserId, StorageRoot);

            var postContent = new PostContent
            {
                CreatedBy = UserTemp.UserId,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = UserTemp.UserId,
                ModifiedDate = DateTime.UtcNow,
                PostId = postContentUpload.PostId,
                MediaId = 0,
                Media = new Media 
                    {
                        ThumbnailContent = null,
                        ThumbnailPath = mediaPath + "tn\\" + Path.GetFileName(postContentUpload.PostContentUploadFile.FileName),
                        MediaContent = bytePostedFile,
                        MediaPath = mediaPath + Path.GetFileName(postContentUpload.PostContentUploadFile.FileName),
                        MediaType = postContentUpload.PostContentUploadFile.ContentType,
                        FileName = postContentUpload.PostContentUploadFile.FileName,
                        ExternalUrl = "",
                        UserId = UserTemp.UserId,
                        CreatedBy = UserTemp.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = UserTemp.UserId,
                        ModifiedDate = DateTime.UtcNow
                    }
            };

            _service.AddPostContent(postContent);
            return RedirectToAction("ModifyPost", "Posts", new { id = postContentUpload.PostId });
        }
    }
}
