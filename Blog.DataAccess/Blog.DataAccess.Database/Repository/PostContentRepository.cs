using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository
{
    public class PostContentRepository : GenericRepository<BlogDb, PostContent>, IPostContentRepository
    {
    }
}
