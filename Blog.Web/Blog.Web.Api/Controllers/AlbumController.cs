using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Common.Utils;

namespace Blog.Web.Api.Controllers
{
    public class AlbumController : ApiController
    {
        private readonly IAlbumResource _service;
        private readonly IErrorSignaler _errorSignaler;

        public AlbumController(IAlbumResource service, IErrorSignaler errorSignaler)
        {
            _service = service;
            _errorSignaler = errorSignaler;
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
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return albums;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/albums/default")]
        public Album GetUserDefault(int userId)
        {
            try
            {
                return _service.GetUserDefaultGroup(userId) ?? new Album();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Album().GenerateError<Album>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPost]
        [Route("api/albums")]
        public Album Post([FromBody]Album album)
        {
            try
            {
                return _service.Add(album);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Album().GenerateError<Album>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPut]
        [Route("api/albums")]
        public Album Put([FromBody]Album album)
        {
            try
            {
                return _service.Add(album);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Album().GenerateError<Album>((int)Constants.Error.InternalError,
                    "Server technical error");
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
