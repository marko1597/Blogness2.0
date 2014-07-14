﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class AlbumResource : BaseResource, IAlbumResource
    {
        public List<Album> GetByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<IAlbumService>("AlbumService"))
            {
                return svc.Proxy.GetByUser(userId);
            }
        }

        public Album GetUserDefaultGroup(int userId)
        {
            using (var svc = new ServiceProxyHelper<IAlbumService>("AlbumService"))
            {
                return svc.Proxy.GetUserDefaultGroup(userId);
            }
        }

        public Album Add(Album album)
        {
            using (var svc = new ServiceProxyHelper<IAlbumService>("AlbumService"))
            {
                return svc.Proxy.Add(album);
            }
        }

        public Album Update(Album album)
        {
            using (var svc = new ServiceProxyHelper<IAlbumService>("AlbumService"))
            {
                return svc.Proxy.Update(album);
            }
        }

        public bool Delete(int albumId)
        {
            using (var svc = new ServiceProxyHelper<IAlbumService>("AlbumService"))
            {
                return svc.Proxy.Delete(albumId);
            }
        }
    }
}