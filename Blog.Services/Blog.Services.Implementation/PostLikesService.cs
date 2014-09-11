using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class PostLikesService : BaseService, IPostLikesService
    {
        private readonly IPostLikesLogic _postLikesLogic;
        private readonly IRedisService _redisService;

        public PostLikesService(IPostLikesLogic postLikesLogic, IRedisService redisService)
        {
            _postLikesLogic = postLikesLogic;
            _redisService = redisService;
        }

        public List<PostLike> Get(int postId)
        {
            return _postLikesLogic.Get(postId);
        }

        public PostLike Add(PostLike postLike)
        {
            var result = _postLikesLogic.Add(postLike);
            if (result.Error == null) return result;

            var postLikes = _postLikesLogic.Get(postLike.PostId);
            var postLikesUpdate = new PostLikesUpdate
            {
                PostId = postLike.PostId,
                PostLikes = postLikes,
                ClientFunction = Constants.SocketClientFunctions.postLikesUpdate.ToString()
            };

            _redisService.Publish(postLikesUpdate);

            return _postLikesLogic.Add(postLike);
        }
    }
}
