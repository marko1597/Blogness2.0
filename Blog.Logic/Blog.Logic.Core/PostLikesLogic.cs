using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class PostLikesLogic : IPostLikesLogic
    {
        private readonly IPostLikeRepository _postLikeRepository;

        public PostLikesLogic(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }

        public List<PostLike> Get(int postId)
        {
            var postLikes = new List<PostLike>();
            try
            {
                var db = _postLikeRepository.Find(a => a.PostId == postId, true).ToList();
                db.ForEach(a => postLikes.Add(PostLikeMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return postLikes;
        }

        public PostLike Add(PostLike postLike)
        {
            try
            {
                var tmpPostLike = _postLikeRepository.Find(a => a.PostId == postLike.PostId && a.UserId == postLike.UserId, false).ToList();
                if (tmpPostLike.Count > 0)
                {
                    _postLikeRepository.Delete(tmpPostLike.FirstOrDefault());
                    return null;
                }

                return PostLikeMapper.ToDto(_postLikeRepository.Add(PostLikeMapper.ToEntity(postLike)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
