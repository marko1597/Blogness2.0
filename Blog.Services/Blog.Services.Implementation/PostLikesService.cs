using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class PostLikesService : BaseService, IPostLikesService
    {
        private readonly IPostLikesLogic _postLikesLogic;

        public PostLikesService(IPostLikesLogic postLikesLogic)
        {
            _postLikesLogic = postLikesLogic;
        }

        public List<PostLike> Get(int postId)
        {
            return _postLikesLogic.Get(postId);
        }

        public PostLike Add(PostLike postLike)
        {
            return _postLikesLogic.Add(postLike);
        }
    }
}
