using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class CommentMock : IComments
    {
        public CommentMock()
        {
            if (DataStorage.Comments.Count == 0)
            {
                var commentId = 1;

                foreach (var p in DataStorage.Posts)
                {
                    for (var i = 1; i < 4; i++)
                    {
                        DataStorage.Comments.Add(new Comment
                        {
                            CommentId = commentId,
                            CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
                            PostId = p.PostId,
                            ParentCommentId = null,
                            CreatedBy = i,
                            CreatedDate = DateTime.UtcNow.AddHours(-i),
                            ModifiedBy = p.User.UserId,
                            ModifiedDate = DateTime.UtcNow.AddHours(-i),
                            User = DataStorage.Users.FirstOrDefault(a => a.UserId == i),
                            CommentLocation = "Makati City, Philippines"
                        });
                        commentId++;
                    }
                }
            }
        }

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
