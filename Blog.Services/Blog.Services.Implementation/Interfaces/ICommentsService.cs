using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface ICommentsService : IBaseService
    {
        [OperationContract]
        Comment Get(int commentId);

        [OperationContract]
        List<Comment> GetByPostId(int postId);

        [OperationContract]
        List<Comment> GetByUser(int userId);

        [OperationContract]
        List<Comment> GetReplies(int commentId);

        [OperationContract]
        Comment Add(Comment comment);

        [OperationContract]
        bool Delete(int commentId);
    }
}
