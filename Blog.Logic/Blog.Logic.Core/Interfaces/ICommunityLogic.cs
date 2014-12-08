using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface ICommunityLogic
    {
        Community Get(int communityId);
        List<Community> GetList();
        List<Community> GetMore(int skip);
        List<Community> GetByUser(int userId);
        List<Community> GetMoreByUser(int userId, int skip);
        Community Add(Community community);
        Community Update(Community community);
        bool Delete(int communityId);
    }
}
