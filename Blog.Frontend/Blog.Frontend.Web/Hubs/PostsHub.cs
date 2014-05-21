using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Microsoft.AspNet.SignalR;

namespace Blog.Frontend.Web.Hubs
{
    public class PostsHub : Hub
    {
        public void PostsLikeUpdate(List<PostLike> postLikes)
        {
            var firstOrDefault = postLikes.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var postId = firstOrDefault.PostId;
                Clients.All.postsLikeUpdate(postId, postLikes);
            }
        }
    }
}