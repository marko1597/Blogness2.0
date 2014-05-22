using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Microsoft.AspNet.SignalR;
using PostsHub = Blog.Web.Site.Hubs.PostsHub;

namespace Blog.Web.Site.Controllers
{
    public class HubController : Controller
    {
        [ActionName("PostLikesUpdate")]
        public void PostLikesUpdate(List<PostLike> postLikes)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<PostsHub>();

            var firstOrDefault = postLikes.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var postId = firstOrDefault.PostId;
                context.Clients.All.postsLikeUpdate(postId, postLikes);
            }
        }
	}
}