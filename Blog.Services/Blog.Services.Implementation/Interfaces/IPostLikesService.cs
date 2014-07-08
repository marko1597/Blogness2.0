using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IPostLikesService : IBaseService
    {
        [OperationContract]
        List<PostLike> Get(int postId);

        [OperationContract]
        PostLike Add(PostLike postLike);
    }
}
