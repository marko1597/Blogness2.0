using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class CommentMock : IComments
    {
        public List<Comment> GetByPostId(int postId)
        {
            var comments = DataStorage.Comments.FindAll(a => a.PostId == postId);
            return comments;
        }

        public List<Comment> GetByUser(int userId)
        {
            var comments = DataStorage.Comments.FindAll(a => a.User.UserId == userId);
            return comments;
        }

        public List<Comment> GetReplies(int commentId)
        {
            var comments = DataStorage.Comments.FindAll(a => a.CommentId == commentId);
            return comments;
        }

        public bool Add(Comment comment)
        {
            var id = DataStorage.Comments.Select(a => a.CommentId).Max();
            comment.CommentId = id + 1;
            DataStorage.Comments.Add(comment);

            return true;
        }

        public bool Delete(int commentId)
        {
            var tComment = DataStorage.Comments.FirstOrDefault(a => a.CommentId == commentId);
            DataStorage.Comments.Remove(tComment);

            return true;
        }
    }
}
