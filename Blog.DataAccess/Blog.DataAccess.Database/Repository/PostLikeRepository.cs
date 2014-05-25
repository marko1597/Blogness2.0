using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class PostLikeRepository : GenericRepository<BlogDb, PostLike>, IPostLikeRepository
    {
    }
}
