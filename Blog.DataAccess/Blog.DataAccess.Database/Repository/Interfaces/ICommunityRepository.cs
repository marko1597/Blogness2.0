using System.Collections.Generic;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository.Interfaces
{
    public interface ICommunityRepository : IGenericRepository<Community>
    {
        IList<Community> GetCommunitiesByUser(int userId, int threshold = 10);
        IList<Community> GetMoreCommunitiesByUser(int userId, int threshold = 5, int skip = 10);
    }
}
