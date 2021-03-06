﻿using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface ICommunityLogic
    {
        Community Get(int communityId);
        List<Community> GetList();
        List<Community> GetMore(int skip);
        List<Community> GetJoinedByUser(int userId);
        List<Community> GetMoreJoinedByUser(int userId, int skip);
        List<Community> GetCreatedByUser(int userId);
        List<Community> GetMoreCreatedByUser(int userId, int skip);
        Community Add(Community community);
        Community Update(Community community);
        bool Delete(int communityId);
    }
}
