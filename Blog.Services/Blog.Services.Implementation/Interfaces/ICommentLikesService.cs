using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface ICommentLikesService : IBaseService
    {
        [OperationContract]
        List<CommentLike> Get(int commentId);

        [OperationContract]
        void Add(CommentLike commentLike);
    }
}
