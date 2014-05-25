using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class AlbumController : ApiController
    {
        private readonly IAlbum _service;

        public AlbumController(IAlbum service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/albums")]
        public List<Album> GetByUser(int userId)
        {
            var albums = new List<Album>();
            try
            {
                albums = _service.GetByUser(userId) ?? new List<Album>();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return albums;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/albums/default")]
        public Album GetUserDefault(int userId)
        {
            var album = new Album();
            try
            {
                album = _service.GetUserDefaultGroup(userId) ?? new Album();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return album;
        }

        [HttpPost]
        [Route("api/albums")]
        public bool Post([FromBody]Album album)
        {
            try
            {
                _service.Add(album);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        [Route("api/albums")]
        public bool Put([FromBody]Album album)
        {
            try
            {
                _service.Add(album);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("api/albums")]
        public bool Delete([FromBody]int albumId)
        {
            try
            {
                _service.Delete(albumId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
