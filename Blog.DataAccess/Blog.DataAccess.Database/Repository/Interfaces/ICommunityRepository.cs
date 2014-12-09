using System.Collections.Generic;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository.Interfaces
{
    public interface ICommunityRepository : IGenericRepository<Community>
    {
        Community Get(int id);
        IList<Community> GetJoinedCommunitiesByUser(int userId, int threshold = 10);
        IList<Community> GetMoreJoinedCommunitiesByUser(int userId, int threshold = 5, int skip = 10);
        IList<Community> GetCreatedCommunitiesByUser(int userId, int threshold = 10);
        IList<Community> GetMoreCreatedCommunitiesByUser(int userId, int threshold = 5, int skip = 10);
    }
}
