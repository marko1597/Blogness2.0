using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class CommunityLogic : ICommunityLogic
    {
        private readonly ICommunityRepository _communityRepository;

        public CommunityLogic(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public Community Get(int communityId)
        {
            try
            {
                var db = _communityRepository.Get(communityId);
                if (db == null)
                {
                    return new Community().GenerateError<Community>((int)Constants.Error.RecordNotFound,
                        string.Format("Cannot find community with Id {0}", communityId));
                }
                
                return CommunityMapper.ToDto(db);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<Community> GetList()
        {
            var communities = new List<Community>();
            try
            {
                var db = _communityRepository.GetList(10).ToList();
                db.ForEach(a => communities.Add(CommunityMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return communities;
        }

        public List<Community> GetMore(int skip)
        {
            var communities = new List<Community>();
            try
            {
                var db = _communityRepository.GetMore(5, 10).ToList();
                db.ForEach(a => communities.Add(CommunityMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return communities;
        }

        public List<Community> GetJoinedByUser(int userId)
        {
            var communities = new List<Community>();
            try
            {
                var db = _communityRepository.GetJoinedCommunitiesByUser(userId).ToList();
                db.ForEach(a => communities.Add(CommunityMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return communities;
        }

        public List<Community> GetMoreJoinedByUser(int userId, int skip)
        {
            var communities = new List<Community>();
            try
            {
                var db = _communityRepository.GetMoreJoinedCommunitiesByUser(userId, 5, skip).ToList();
                db.ForEach(a => communities.Add(CommunityMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return communities;
        }

        public List<Community> GetCreatedByUser(int userId)
        {
            var communities = new List<Community>();
            try
            {
                var db = _communityRepository.GetCreatedCommunitiesByUser(userId).ToList();
                db.ForEach(a => communities.Add(CommunityMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return communities;
        }

        public List<Community> GetMoreCreatedByUser(int userId, int skip)
        {
            var communities = new List<Community>();
            try
            {
                var db = _communityRepository.GetMoreCreatedCommunitiesByUser(userId, 5, skip).ToList();
                db.ForEach(a => communities.Add(CommunityMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return communities;
        }

        public Community Add(Community community)
        {
            try
            {
                var checkCommunity = IsCommunityNameInUse(community.Name);
                if (checkCommunity)
                {
                    return new Community().GenerateError<Community>((int)Constants.Error.ValidationError,
                        string.Format("Community name {0} is already in use.", community.Name));
                }

                return CommunityMapper.ToDto(_communityRepository.Add(CommunityMapper.ToEntity(community)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Community Update(Community community)
        {
            try
            {
                var checkCommunity = IsCommunityNameInUse(community.Name);
                if (checkCommunity)
                {
                    return new Community().GenerateError<Community>((int)Constants.Error.ValidationError,
                        string.Format("Community name {0} is already in use.", community.Name));
                }

                return CommunityMapper.ToDto(_communityRepository.Edit(CommunityMapper.ToEntity(community)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int communityId)
        {
            try
            {
                var db = _communityRepository.Find(a => a.Id == communityId, false).FirstOrDefault();
                if (db == null) return false;

                _communityRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private bool IsCommunityNameInUse(string name)
        {
            var dbCommunity = _communityRepository.Find(a => a.Name == name, null, null).FirstOrDefault();
            return dbCommunity != null;
        }
    }
}
