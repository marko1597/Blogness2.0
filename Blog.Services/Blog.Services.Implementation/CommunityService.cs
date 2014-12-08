using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class CommunityService : BaseService, ICommunityService
    {
        private readonly ICommunityLogic _communityLogic;

        public CommunityService(ICommunityLogic communityLogic)
        {
            _communityLogic = communityLogic;
        }

        public Community Get(int communityId)
        {
            return _communityLogic.Get(communityId);
        }

        public List<Community> GetList()
        {
            return _communityLogic.GetList();
        }

        public List<Community> GetMore(int skip)
        {
            return _communityLogic.GetMore(skip);
        }

        public List<Community> GetJoinedByUser(int userId)
        {
            return _communityLogic.GetJoinedByUser(userId);
        }

        public List<Community> GetMoreJoinedByUser(int userId, int skip)
        {
            return _communityLogic.GetMoreJoinedByUser(userId, skip);
        }

        public List<Community> GetCreatedByUser(int userId)
        {
            return _communityLogic.GetCreatedByUser(userId);
        }

        public List<Community> GetMoreCreatedByUser(int userId, int skip)
        {
            return _communityLogic.GetMoreCreatedByUser(userId, skip);
        }

        public Community Add(Community community)
        {
            return _communityLogic.Add(community);
        }

        public Community Update(Community community)
        {
            return _communityLogic.Update(community);
        }

        public bool Delete(int communityId)
        {
            return _communityLogic.Delete(communityId);
        }
    }
}
