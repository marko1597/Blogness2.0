using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class AlbumService : BaseService, IAlbumService
    {
        private readonly IAlbumLogic _albumLogic;

        public AlbumService(IAlbumLogic albumLogic)
        {
            _albumLogic = albumLogic;
        }

        public Album Get(int id)
        {
            return _albumLogic.Get(id);
        }

        public List<Album> GetByUser(int userId)
        {
            return _albumLogic.GetByUser(userId);
        }

        public Album GetUserDefaultGroup(int userId)
        {
            return _albumLogic.GetUserDefaultGroup(userId);
        }

        public Album Add(Album album)
        {
            return _albumLogic.Add(album);
        }

        public Album Update(Album album)
        {
            return _albumLogic.Update(album);
        }

        public bool Delete(int albumId)
        {
            return _albumLogic.Delete(albumId);
        }
    }
}
