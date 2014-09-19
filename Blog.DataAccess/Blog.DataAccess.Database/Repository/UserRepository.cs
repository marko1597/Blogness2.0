using System.Collections.Generic;
using System.Linq;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class UserRepository : GenericRepository<BlogDb, User>, IUserRepository
    {
        public IList<User> GetUsers(int threshold = 10, int skip = 10)
        {
            var query = Find(a => a.UserId != 0, null, null)
                .OrderByDescending(b => b.UserId)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }
    }
}
