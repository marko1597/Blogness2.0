using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class MediaRestResource : IMediaResource
    {
        public bool GetHeartBeat()
        {
            throw new System.NotImplementedException();
        }

        public List<Media> GetByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Media> GetByGroup(int albumId)
        {
            throw new System.NotImplementedException();
        }

        public Media GetByName(string customName)
        {
            throw new System.NotImplementedException();
        }

        public Media Get(int mediaId)
        {
            throw new System.NotImplementedException();
        }

        public Media Add(Media media, int userId)
        {
            throw new System.NotImplementedException();
        }

        public Media AddAsContent(User user, string albumName, string filename, string path, string contentType)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int mediaId)
        {
            throw new System.NotImplementedException();
        }
    }
}
