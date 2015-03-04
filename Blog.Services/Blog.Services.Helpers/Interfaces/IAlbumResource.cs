using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IAlbumResource : IAlbumService
    {
    }

    public interface IAlbumRestResource
    {
        Album Get(int id);
        List<Album> GetByUser(int userId);
        Album GetUserDefaultGroup(int userId, string authenticationToken);
        Album Add(Album album, string authenticationToken);
        Album Update(Album album, string authenticationToken);
        bool Delete(int albumId, string authenticationToken);
    }
}
