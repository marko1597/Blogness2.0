using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IAlbumLogic
    {
        Album Get(int id);
        List<Album> GetByUser(int userId);
        Album GetUserDefaultGroup(int userId);
        bool Delete(int albumId);
        Album Add(Album album);
        Album Update(Album album);
    }
}
