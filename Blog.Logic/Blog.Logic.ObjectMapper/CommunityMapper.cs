using System.Linq;
using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class CommunityMapper
    {
        public static Community ToDto(Db.Community community)
        {
            if (community == null) return null;

            var members = community.Members != null
                ? community.Members.Select(UserMapper.ToDto).ToList()
                : null;
            var posts = community.Posts != null
                ? community.Posts.Select(PostMapper.ToDto).ToList()
                : null;

            return new Community
                {
                    Id = community.Id,
                    Name = community.Name,
                    Description = community.Description,
                    Leader = UserMapper.ToDto(community.Leader),
                    IsDeleted = community.IsDeleted,
                    Members = members,
                    Posts = posts,
                    CreatedBy = community.CreatedBy,
                    CreatedDate = community.CreatedDate,
                    ModifiedBy = community.ModifiedBy,
                    ModifiedDate = community.ModifiedDate
                };
        }

        public static Db.Community ToEntity(Community community)
        {
            if (community == null) return null;

            var members = community.Members != null
                ? community.Members.Select(UserMapper.ToEntity).ToList()
                : null;
            var posts = community.Posts != null
                ? community.Posts.Select(PostMapper.ToEntity).ToList()
                : null;

            return new Db.Community
                {
                    Id = community.Id,
                    Name = community.Name,
                    Description = community.Description,
                    Members = members,
                    Posts = posts,
                    IsDeleted = community.IsDeleted,
                    LeaderUserId = community.Leader != null ? community.Leader.Id : 0,
                    Leader = null,
                    CreatedBy = community.CreatedBy,
                    CreatedDate = community.CreatedDate,
                    ModifiedBy = community.ModifiedBy,
                    ModifiedDate = community.ModifiedDate
                };
        }
    }
}
