using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class AlbumRestResource : IAlbumRestResource
    {
        public Album Get(int id)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Album>(svc.Get(Constants.BlogRestUrl, string.Format("album/{0}", id)));
                return result;
            }
        }

        public List<Album> GetByUser(int userId)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<List<Album>>(svc.Get(Constants.BlogRestUrl, string.Format("users/{0}/albums", userId)));
                return result;
            }
        }

        public Album GetUserDefaultGroup(int userId, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Album>(svc.Get(Constants.BlogRestUrl, string.Format("users/{0}/albums/default", userId), authenticationToken));
                return result;
            }
        }

        public Album Add(Album album, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Album>(svc.Post(Constants.BlogRestUrl, "album", album, authenticationToken));
                return result;
            }
        }

        public Album Update(Album album, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Album>(svc.Put(Constants.BlogRestUrl, "album", album, authenticationToken));
                return result;
            }
        }

        public bool Delete(int albumId, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<bool>(svc.Delete(Constants.BlogRestUrl, string.Format("/album/{0}", albumId), authenticationToken));
                return result;
            }
        }
    }
}
