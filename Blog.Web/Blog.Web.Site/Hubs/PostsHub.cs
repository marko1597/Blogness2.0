using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task ViewPost(int postId)
        {
            return Groups.Add(Context.ConnectionId, "post_" + postId);
        }
    }
}