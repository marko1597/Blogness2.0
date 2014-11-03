using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUsersResource _usersService;
        private readonly IErrorSignaler _errorSignaler;

        public AlbumController(IAlbumResource service, IUsersResource usersService, IErrorSignaler errorSignaler)
        {
            _service = service;
            _usersService = usersService;
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
        public IHttpActionResult GetByUser(int userId)
        {
            
            try
            {
                var albums = _service.GetByUser(userId) ?? new List<Album>();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/users/{username}/{albumName}")]
        public IHttpActionResult GetByUser(string username, string albumName)
        {
            try
            {
                var user = _usersService.GetByUserName(username);
                if (user == null) return Ok(new Album().GenerateError<Album>((int)Constants.Error.RecordNotFound, "User not found!"));

                var albumList = _service.GetByUser(user.Id);

                if (albumList == null || albumList.Count == 0)
                {
                    return Ok(new Album().GenerateError<Album>((int)Constants.Error.RecordNotFound, "Album not found!"));
                }
                
                var albumByName = albumList.Where(a => a.AlbumName == albumName).First();

                if (albumByName != null) return Ok(albumByName);

                return Ok(new Album().GenerateError<Album>((int)Constants.Error.RecordNotFound, "Album not found!"));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/users/{userId:int}/albums/default")]
        public IHttpActionResult GetUserDefault(int userId)
        {
            try
            {
                var result = _service.GetUserDefaultGroup(userId);
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
        public IHttpActionResult Delete(int albumId)
        {
            try
            {
                var album = _service.Get(albumId);
                if (album != null && album.Error != null)
                {
                    _errorSignaler.SignalFromCurrentContext(new Exception(album.Error.Message));
                    return Ok(false);
                }

                if (album == null || album.User == null) return Ok(false);

                var username = HttpContext.Current.User.Identity.Name;
                if (album.User.UserName != username) return Ok(false);

                _service.Delete(albumId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return Ok(false);
            }
        }
    }
}
