using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
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
            context.Clients.All.postsLikeUpdate(postLikesUpdate.PostId, postLikesUpdate.PostLikes);
        }
	}
}