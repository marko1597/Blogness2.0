using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository
{
    public class MediaRepository : GenericRepository<BlogDb, Media>, IMediaRepository
    {
    }
}
