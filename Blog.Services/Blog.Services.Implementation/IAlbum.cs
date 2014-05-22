using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation
{
    public interface IAlbum
    {
        List<Album> GetByUser(int userId);
        Album GetUserDefaultGroup(int userId);
        bool Add(Album album);
        bool Update(Album album);
        bool Delete(int albumId);
    }
}
