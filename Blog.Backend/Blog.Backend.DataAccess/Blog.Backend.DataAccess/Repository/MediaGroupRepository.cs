using Blog.Backend.DataAccess.Entities;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Repository
{
    public class MediaGroupRepository : GenericRepository<BlogDb, MediaGroup>, IMediaGroupRepository
    {
    }
}
