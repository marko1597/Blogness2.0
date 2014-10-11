using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IViewCountService : IBaseService
    {
        [OperationContract]
        List<ViewCount> Get(int postId);

        [OperationContract]
        void Add(ViewCount viewCount);
    }
}
