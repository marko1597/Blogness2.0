using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class MediaResource : IMediaResource
    {
        public List<Media> GetByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.GetByUser(userId);
            }
        }

        public Media Get(int mediaId)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.Get(mediaId);
            }
        }

        public List<Media> GetByGroup(int albumId)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.GetByGroup(albumId);
            }
        }

        public Media GetByName(string customName)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.GetByName(customName);
            }
        }

        public Media Add(Media media, int userId)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.Add(media, userId);
            }
        }

        public Media AddAsContent(User user, string albumName, string filename, string path, string contentType)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.AddAsContent(user, albumName, filename, path, contentType);
            }
        }
        
        public bool Delete(int mediaId)
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.Delete(mediaId);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IMediaService>("MediaService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
