using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IAlbumResource : IBaseResource
    {
        List<Album> GetByUser(int userId);
        Album GetUserDefaultGroup(int userId);
        Album Add(Album album);
        Album Update(Album album);
        bool Delete(int albumId);
    }
}
