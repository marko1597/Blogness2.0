using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
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

        public void Add(PostLike postLike)
        {
            var result = _postLikesLogic.Add(postLike);
            if (result != null && result.Error != null) throw new Exception(result.Error.Message);

            var postLikes = _postLikesLogic.Get(postLike.PostId);
            var postLikesUpdate = new PostLikesUpdate
            {
                PostId = postLike.PostId,
                PostLikes = postLikes,
                ClientFunction = Constants.SocketClientFunctions.PostLikesUpdate.ToString()
            };

            _redisService.Publish(postLikesUpdate);
        }
    }
}
