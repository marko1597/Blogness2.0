using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class CommentsRestResource : ICommentsRestResource
    {
        public Comment Get(int commentId)
        {
            throw new System.NotImplementedException();
        }

        public List<Comment> GetByPostId(int postId)
        {
            throw new System.NotImplementedException();
        }

        public List<Comment> GetByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Comment> GetReplies(int commentId)
        {
            throw new System.NotImplementedException();
        }

        public Comment Add(Comment comment, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int commentId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
