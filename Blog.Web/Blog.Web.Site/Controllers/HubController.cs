using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Web.Site.Hubs;
using Microsoft.AspNet.SignalR;
using PostsHub = Blog.Web.Site.Hubs.PostsHub;

namespace Blog.Web.Site.Controllers
{
    public class HubController : Controller
    {
        [ActionName("PostLikesUpdate")]
        public void PostLikesUpdate(PostLikesUpdate postLikesUpdate)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<PostsHub>();
            postLikesUpdate.PostLikes = postLikesUpdate.PostLikes ?? new List<PostLike>();
            context.Clients.All.postLikesUpdate(postLikesUpdate.PostId, postLikesUpdate.PostLikes);
        }

        [ActionName("CommentLikesUpdate")]
        public void CommentLikesUpdate(CommentLikesUpdate commentLikesUpdate)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CommentsHub>();
            commentLikesUpdate.CommentLikes = commentLikesUpdate.CommentLikes ?? new List<CommentLike>();
            context.Clients.All.commentLikesUpdate(commentLikesUpdate.CommentId, commentLikesUpdate.CommentLikes);
        }

        [ActionName("CommentAddedForPost")]
        public void CommentAddedForPost(CommentAdded commentAdded)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CommentsHub>();
            context.Clients.Group("post_" + commentAdded.PostId).commentAdded(commentAdded.PostId, commentAdded.Comment);
        }
	}
}