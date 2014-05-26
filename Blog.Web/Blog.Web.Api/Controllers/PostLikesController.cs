using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Web.Attributes;
using Blog.Services.Implementation.Interfaces;
using PostsHubFactory = Blog.Web.Api.Helper.Hub.Factory.PostsHubFactory;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class PostLikesController : ApiController
    {
        private readonly IPostLikes _service;
        private readonly IUser _user;

        public PostLikesController(IPostLikes service, IUser user)
        {
            _service = service;
            _user = user;
        }

        [HttpGet]
        [Route("api/posts/{postId}/likes")]
        public List<PostLike> Get(int postId)
        {
            var postLikes = new List<PostLike>();

            try
            {
                postLikes = _service.Get(postId) ?? new List<PostLike>();

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return postLikes;
        }

        [HttpPost]
        [Route("api/posts/likes")]
        public void Post([FromUri]int postId, string username)
        {
            try
            {
                var user = _user.GetByUserName(username);
                var postLike = new PostLike
                               {
                                   PostId = postId,
                                   UserId = user.UserId
                               };
                _service.Add(postLike);
                PostsHubFactory.GetInstance().Create().PushPostLikes(new PostLikesUpdate
                                                                     {
                                                                         PostId = postId,
                                                                         PostLikes = _service.Get(postId)
                                                                     });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
