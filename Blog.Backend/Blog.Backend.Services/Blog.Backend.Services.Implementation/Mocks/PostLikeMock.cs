using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class PostLikeMock : IPostLikes
    {
        public PostLikeMock()
        {
            if (DataStorage.PostLikes.Count == 0)
            {
                var postLikeId = 1;

                foreach (var p in DataStorage.PostLikes)
                {
                    for (var i = 1; i < 4; i++)
                    {
                        DataStorage.PostLikes.Add(new PostLike
                                                {
                                                    CreatedBy = i,
                                                    CreatedDate = DateTime.UtcNow.AddHours(-i),
                                                    ModifiedBy = i,
                                                    ModifiedDate = DateTime.UtcNow.AddHours(-i),
                                                    PostId = p.PostId,
                                                    UserId = i,
                                                    PostLikeId = postLikeId
                                                });
                        postLikeId++;
                    }
                }
            }
        }

        public List<PostLike> Get(int postId)
        {
            var postLikes = DataStorage.PostLikes.FindAll(a => a.PostId == postId);
            return postLikes;
        }

        public void Add(PostLike postLike)
        {
            var tpostLike = DataStorage.PostLikes.FindAll(a => a.PostId == postLike.PostId && a.UserId == postLike.UserId);

            if (tpostLike.Count > 0)
            {
                DataStorage.PostLikes.Remove(tpostLike.First());
            }
            else
            {
                var id = DataStorage.PostLikes.Select(a => a.PostLikeId).Max();
                postLike.PostLikeId = id + 1;
                DataStorage.PostLikes.Add(postLike);
            }
        }
    }
}
