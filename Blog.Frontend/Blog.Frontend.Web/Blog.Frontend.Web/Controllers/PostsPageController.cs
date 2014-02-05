using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using Blog.Frontend.Web.CustomHelpers.Authentication;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Frontend.Web.Controllers
{
    [HandleError(View = "Error/Index")]
    public class PostsPageController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public PostsPageController(IBlogService service)
        {
            _service = service;
        }

        #endregion

        public ActionResult PopularPosts()
        {
            var posts = _service.GetPopularPosts(20);
            posts.ForEach(a => { a.PostContents = _service.GetPostContents(a.PostId) ?? new List<PostContent>(); });

            return View("PopularPosts", posts);
        }

        public ActionResult RecentPosts()
        {
            var posts = _service.GetRecentPosts(20);
            posts.ForEach(a => { a.PostContents = _service.GetPostContents(a.PostId) ?? new List<PostContent>(); });

            return View("RecentPosts", posts);
        }

        [CustomAuthorizationAttribute]
        public ActionResult UserPosts()
        {
            var userPosts = _service.GetUserPosts(UserTemp.UserId);
            userPosts.Posts.ForEach(a => { a.PostContents = _service.GetPostContents(a.PostId) ?? new List<PostContent>(); });

            return View("UserPosts", userPosts);
        }
    }
}
