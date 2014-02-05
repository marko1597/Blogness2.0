using System;
using System.Configuration;
using System.Web.Mvc;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using Blog.Frontend.Web.CustomHelpers.Authentication;
using Blog.Frontend.Web.Models;

namespace Blog.Frontend.Web.Controllers
{
    public class PostsController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public PostsController(IBlogService service)
        {
            _service = service;
        }

        #endregion

        #region Misc Actions

        public ActionResult Index()
        {
            return View();
        }

        public RedirectToRouteResult ReturnToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Post Likes

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LikePost(int postId)
        {
            var isLoggedIn = _service.IsLoggedIn(UserTemp.UserId);
            if (isLoggedIn.Token != null)
            {
                var postLike = new PostLike
                {
                    UserId = UserTemp.UserId,
                    PostId = postId,
                    CreatedBy = UserTemp.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = UserTemp.UserId,
                    ModifiedDate = DateTime.UtcNow
                };
                _service.LikePost(postLike);

                var postLikes = _service.GetPostLikes(postId);
                return PartialView("_Likes", new Likes { Id = postId, Count = postLikes.Count, UserId = UserTemp.UserId });
            }

            return null;
        }

        #endregion

        #region View

        public ActionResult ViewPost(string postId)
        {
            var post = _service.GetPost(Convert.ToInt32(postId));
            post.Comments = _service.GetComments(post.PostId);

            return View("ViewPost", post);
        }

        #endregion

        #region Add

        [CustomAuthorizationAttribute]
        public ActionResult AddPost()
        {
            var post = _service.AddNewPost(UserTemp.UserId);
            return View("AddPost", post);
        }

        #endregion

        #region Modify

        [CustomAuthorizationAttribute]
        public ActionResult ModifyPost()
        {
            var model = _service.GetPost(Convert.ToInt32(RouteData.Values["id"].ToString()));
            return View("ModifyPost", model);
        }

        [CustomAuthorizationAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        [JsonFilter(Param = "data", RootType = typeof(PostUpdate))]
        public ActionResult SavePost(PostUpdate post)
        {
            var tPost = _service.GetPost(post.PostId);
            tPost.PostTitle = post.PostTitle;
            tPost.PostMessage = post.PostMessage;
            tPost.ModifiedBy = UserTemp.UserId;
            tPost.ModifiedDate = DateTime.UtcNow;

            _service.ModifyPost(tPost);
            return null;
        }


        #endregion

        #region Delete

        [CustomAuthorizationAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeletePost(int postId)
        {
            _service.DeletePost(postId);
            return RedirectToAction("UserPosts", "PostsPage");
        }

        #endregion

        #region Private Members

        [OutputCache(Duration = 60, VaryByParam = "userId")]
        private UserPosts GetUserPosts(int userId)
        {
            var userposts = _service.GetUserPosts(userId);
            return userposts;
        }

        public ActionResult GetPostImageModifyJson(PostContentModify postContentModify)
        {
            return Json(postContentModify);
        }

        public string StorageRoot
        {
            get { return ConfigurationManager.AppSettings["StorageRoot"]; }
        }

        #endregion
    }
}
