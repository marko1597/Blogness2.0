using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface ICommunityResource : ICommunityService
    {
    }

    public interface ICommunityRestResource
    {
        Community Get(int communityId);
        List<Community> GetList();
        List<Community> GetMore(int skip);
        List<Community> GetJoinedByUser(int userId);
        List<Community> GetMoreJoinedByUser(int userId, int skip);
        List<Community> GetCreatedByUser(int userId);
        List<Community> GetMoreCreatedByUser(int userId, int skip);
        Community Add(Community community, string authenticationToken);
        Community Update(Community community, string authenticationToken);
        bool Delete(int communityId, string authenticationToken);
    }
}
