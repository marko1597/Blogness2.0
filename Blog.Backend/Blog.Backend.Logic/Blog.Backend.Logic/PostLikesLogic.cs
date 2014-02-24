using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class PostLikesLogic
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
                Console.WriteLine(ex.Message);
            }
            return postLikes;
        }

        public void Add(PostLike postLike)
        {
            try
            {
                var tmpPostLike = _postLikeRepository.Find(a => a.PostId == postLike.PostId && a.UserId == postLike.UserId, true).ToList();
                if (tmpPostLike.Count > 0)
                {
                    _postLikeRepository.Delete(tmpPostLike.FirstOrDefault());
                }
                else
                {
                    _postLikeRepository.Add(PostLikeMapper.ToEntity(postLike));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
