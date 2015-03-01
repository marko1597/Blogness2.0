using System.Collections.Generic;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IList<User> GetUsers(int threshold = 10, int skip = 10);
        IList<User> GetUsersByCommunity(int communityId, int threshold = 10, int skip = 10);
    }
}
