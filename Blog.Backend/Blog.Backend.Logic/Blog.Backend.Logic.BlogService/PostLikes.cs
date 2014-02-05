using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class PostLikes
    {
        private readonly IPostLikeResource _postLikeResource;

        public PostLikes(IPostLikeResource postLikeResource)
        {
            _postLikeResource = postLikeResource;
        }

        public List<PostLike> Get(int postId)
        {
            var postLikes = new List<PostLike>();
            try
            {
                postLikes = _postLikeResource.Get(a => a.PostId == postId);
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
                var tmpPostLike = _postLikeResource.Get(a => a.PostId == postLike.PostId && a.UserId == postLike.UserId);
                if (tmpPostLike.Count > 0)
                {
                    _postLikeResource.Delete(tmpPostLike.FirstOrDefault());
                }
                else
                {
                    _postLikeResource.Add(postLike);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
