using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Microsoft.AspNet.SignalR;

namespace Blog.Web.Site.Hubs
{
    public class CommentsHub : Hub
    {
        public void CommentsLikeUpdate(List<CommentLike> commentLikes)
        {
            var firstOrDefault = commentLikes.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var postId = firstOrDefault.CommentId;
                Clients.All.commentLikesUpdate(postId, commentLikes);
            }
        }

        public void CommentAddedForPost(CommentAdded commentAdded)
        {
            Clients.All.commentAddedForPost(commentAdded.PostId, commentAdded.Comment);
        }
        
        public void ViewPost(int postId)
        {
            Groups.Add(Context.ConnectionId, "post_" + postId);
        }
    }
}