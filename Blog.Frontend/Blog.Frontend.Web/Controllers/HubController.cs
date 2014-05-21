﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Backend.Common.Contracts;
using Microsoft.AspNet.SignalR;
using PostsHub = Blog.Frontend.Web.Hubs.PostsHub;

namespace Blog.Frontend.Web.Controllers
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