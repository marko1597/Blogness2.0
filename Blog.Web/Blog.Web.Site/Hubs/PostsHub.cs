using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Microsoft.AspNet.SignalR;

namespace Blog.Web.Site.Hubs
{
    public class PostsHub : Hub
    {
        public void PostsLikeUpdate(List<PostLike> postLikes)
        {
            var firstOrDefault = postLikes.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var postId = firstOrDefault.PostId;
                Clients.All.postLikesUpdate(postId, postLikes);
            }
        }
    }
}