using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Common.Utils;
using Blog.Common.Web.Attributes;

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
        [Route("api/album/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var album = _service.Get(id);
                return Ok(album);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
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

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/album")]
        public IHttpActionResult Post([FromBody]Album album)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Add(album);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Album().GenerateError<Album>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
        [Route("api/album")]
        public IHttpActionResult Put([FromBody]Album album)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Update(album);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Album().GenerateError<Album>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/album/{albumId}")]
        public bool Delete(int albumId)
        {
            try
            {
                var album = _service.Get(albumId);
                if (album != null && album.Error != null)
                {
                    _errorSignaler.SignalFromCurrentContext(new Exception(album.Error.Message));
                    return false;
                }

                if (album != null && album.User != null)
                {
                    var username = HttpContext.Current.User.Identity.Name;
                    if (album.User.UserName == username)
                    {
                        _service.Delete(albumId);
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
