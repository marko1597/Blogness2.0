using Blog.Backend.DataAccess.Entities;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Repository
{
    public class UserRepository : GenericRepository<BlogDb, User>, IUserRepository
    {
    }
}
