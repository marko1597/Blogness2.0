using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface ICommunityService : IBaseService
    {
        [OperationContract]
        Community Get(int communityId);

        [OperationContract]
        List<Community> GetList();

        [OperationContract]
        List<Community> GetMore(int skip);

        [OperationContract]
        List<Community> GetJoinedByUser(int userId);

        [OperationContract]
        List<Community> GetMoreJoinedByUser(int userId, int skip);

        [OperationContract]
        List<Community> GetCreatedByUser(int userId);

        [OperationContract]
        List<Community> GetMoreCreatedByUser(int userId, int skip);

        [OperationContract]
        Community Add(Community community);

        [OperationContract]
        Community Update(Community community);

        [OperationContract]
        bool Delete(int communityId);
    }
}
