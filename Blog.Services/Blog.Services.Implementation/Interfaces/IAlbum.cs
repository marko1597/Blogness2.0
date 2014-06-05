using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IAlbum
    {
        List<Album> GetByUser(int userId);
        Album GetUserDefaultGroup(int userId);
        Album Add(Album album);
        Album Update(Album album);
        bool Delete(int albumId);
    }
}
