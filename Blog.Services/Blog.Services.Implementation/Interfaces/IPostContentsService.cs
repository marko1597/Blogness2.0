using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IPostContentsService : IBaseService
    {
        [OperationContract]
        List<PostContent> GetByPostId(int postId);

        [OperationContract]
        PostContent Get(int postContentId);

        [OperationContract]
        PostContent Add(PostContent postContent);

        [OperationContract]
        bool Delete(int postContentId);
    }
}
