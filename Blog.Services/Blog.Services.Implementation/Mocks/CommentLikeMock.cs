using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class CommentLikeMock : ICommentLikes
    {
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
