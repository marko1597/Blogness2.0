using System;
using System.Web.Mvc;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Frontend.Services;
using Blog.Frontend.Web.CustomHelpers.Attributes;
using Blog.Frontend.Web.CustomHelpers.Authentication;
using Blog.Frontend.Web.Models;

namespace Blog.Frontend.Web.Controllers
{
    public class CommentsController : Controller
    {
        #region Constructor

        private readonly IBlogService _service;

        public CommentsController(IBlogService service)
        {
            _service = service;
        }

        #endregion

        #region Comment Related Actions

        public ActionResult GetComments(int postId)
        {
            return PartialView("_CommentList", _service.GetComments(postId));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [JsonFilter(Param = "data", RootType = typeof(CommentAdd))]
        public ActionResult AddComment(CommentAdd commentAdd)
        {
            var isLoggedIn = _service.IsLoggedIn(UserTemp.UserId);
            if (isLoggedIn.Token != null)
            {
                var comment = new Comment
                {
                    User = new User
                    {
                        UserId = UserTemp.UserId
                    },
                    CommentMessage = commentAdd.CommentMessage,
                    PostId = commentAdd.PostId,
                    ParentCommentId = null,
                    Comments = null,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = UserTemp.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = UserTemp.UserId,
                    CommentLocation = commentAdd.CommentLocation
                };
                _service.AddComment(comment);
                var comments = _service.GetComments((int)comment.PostId);

                return PartialView("_CommentList", new CommentView { Comments = comments, PostId = (int)comment.PostId, IsCommentReply = false });
            }

            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [JsonFilter(Param = "data", RootType = typeof(CommentAdd))]
        public ActionResult AddCommentReply(CommentAdd commentAdd)
        {
            var isLoggedIn = _service.IsLoggedIn(UserTemp.UserId);
            if (isLoggedIn.Token != null)
            {
                var comment = new Comment
                {
                    User = new User
                    {
                        UserId = UserTemp.UserId
                    },
                    CommentMessage = commentAdd.CommentMessage,
                    PostId = null,
                    ParentCommentId = commentAdd.ParentCommentId,
                    Comments = null,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = UserTemp.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = UserTemp.UserId,
                    CommentLocation = commentAdd.CommentLocation
                };
                _service.AddComment(comment);
                var comments = _service.GetCommentReplies((int)commentAdd.ParentCommentId);

                return PartialView("_CommentList", new CommentView { Comments = comments, PostId = null, IsCommentReply = true });
            }

            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [JsonFilter(Param = "data", RootType = typeof(CommentAdd))]
        public ActionResult LikeComment(int commentId)
        {
            var isLoggedIn = _service.IsLoggedIn(UserTemp.UserId);
            if (isLoggedIn.Token != null)
            {
                var commentLike = new CommentLike
                {
                    UserId = UserTemp.UserId,
                    CommentId = commentId,
                    CreatedBy = UserTemp.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = UserTemp.UserId,
                    ModifiedDate = DateTime.UtcNow
                };
                _service.LikeComment(commentLike);

                var commentLikes = _service.GetCommentLikes(commentId);
                return PartialView("_Likes", new Likes { Id = commentId, Count = commentLikes.Count, UserId = UserTemp.UserId });
            }
            return null;
        }

        #endregion

        #region Get View Models

        public ActionResult GetCommentToAdd(CommentAdd commentAdd)
        {
            return Json(commentAdd);
        }

        #endregion
    }
}
