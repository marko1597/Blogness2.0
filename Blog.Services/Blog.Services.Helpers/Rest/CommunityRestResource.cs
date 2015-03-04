using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class CommunityRestResource : ICommunityRestResource
    {
        public Community Get(int communityId)
        {
            throw new System.NotImplementedException();
        }

        public List<Community> GetList()
        {
            throw new System.NotImplementedException();
        }

        public List<Community> GetMore(int skip)
        {
            throw new System.NotImplementedException();
        }

        public List<Community> GetJoinedByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Community> GetMoreJoinedByUser(int userId, int skip)
        {
            throw new System.NotImplementedException();
        }

        public List<Community> GetCreatedByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Community> GetMoreCreatedByUser(int userId, int skip)
        {
            throw new System.NotImplementedException();
        }

        public Community Add(Community community, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public Community Update(Community community, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int communityId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
