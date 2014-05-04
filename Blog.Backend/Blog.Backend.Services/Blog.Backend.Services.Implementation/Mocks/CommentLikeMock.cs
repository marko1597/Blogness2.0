using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class CommentLikeMock : ICommentLikes
    {

        public CommentLikeMock()
        {
            if (DataStorage.CommentLikes.Count == 0)
            {
                var commentLikeId = 1;

                foreach (var p in DataStorage.CommentLikes)
                {
                    for (var i = 1; i < 4; i++)
                    {
                        DataStorage.CommentLikes.Add(new CommentLike
                                                {
                                                    CreatedBy = i,
                                                    CreatedDate = DateTime.UtcNow.AddHours(-i),
                                                    ModifiedBy = i,
                                                    ModifiedDate = DateTime.UtcNow.AddHours(-i),
                                                    CommentId = p.CommentId,
                                                    UserId = i,
                                                    CommentLikeId = commentLikeId
                                                });
                        commentLikeId++;
                    }
                }
            }
        }

        public List<CommentLike> Get(int commentId)
        {
            var commentLikes = DataStorage.CommentLikes.FindAll(a => a.CommentId == commentId);
            return commentLikes;
        }

        public void Add(CommentLike commentLike)
        {
            var tCommentLike = DataStorage.CommentLikes.FindAll(a => a.CommentId == commentLike.CommentId && a.UserId == commentLike.UserId);

            if (tCommentLike.Count > 0)
            {
                DataStorage.CommentLikes.Remove(tCommentLike.First());
            }
            else
            {
                var id = DataStorage.CommentLikes.Select(a => a.CommentLikeId).Max();
                commentLike.CommentLikeId = id + 1;
                DataStorage.CommentLikes.Add(commentLike);
            }
            
        }
    }
}
