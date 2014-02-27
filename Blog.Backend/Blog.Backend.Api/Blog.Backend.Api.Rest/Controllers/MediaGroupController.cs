using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class MediaGroupController : ApiController
    {
        private readonly IMediaGroup _service;

        public MediaGroupController(IMediaGroup service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/mediagroups")]
        public List<MediaGroup> GetByUser(int userId)
        {
            var mediagroups = new List<MediaGroup>();
            try
            {
                mediagroups = _service.GetByUser(userId) ?? new List<MediaGroup>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mediagroups;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/mediagroups/default")]
        public MediaGroup GetUserDefault(int userId)
        {
            var media = new MediaGroup();
            try
            {
                media = _service.GetUserDefaultGroup(userId) ?? new MediaGroup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        [HttpPost]
        [Route("api/mediagroup")]
        public bool Post([FromBody]MediaGroup media)
        {
            try
            {
                _service.Add(media);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        [Route("api/mediagroup")]
        public bool Put([FromBody]MediaGroup media)
        {
            try
            {
                _service.Add(media);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("api/mediagroup")]
        public bool Delete([FromBody]int mediaGroupId)
        {
            try
            {
                _service.Delete(mediaGroupId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
