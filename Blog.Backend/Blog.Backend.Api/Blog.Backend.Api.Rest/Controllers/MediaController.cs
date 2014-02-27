using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class MediaController : ApiController
    {
        private readonly IMedia _service;

        public MediaController(IMedia service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/mediagroup/{mediaGroupId:int}/media")]
        public List<Media> GetByGroup(int mediaGroupId)
        {
            var media = new List<Media>();
            try
            {
                media = _service.GetByGroup(mediaGroupId) ?? new List<Media>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/media")]
        public List<Media> GetByUser(int userId)
        {
            var media = new List<Media>();
            try
            {
                media = _service.GetByUser(userId) ?? new List<Media>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        [HttpGet]
        [Route("api/media/{mediaId:int}")]
        public HttpResponseMessage Get(int mediaId)
        {
            try
            {
                var media = _service.Get(mediaId) ?? new Media();
                return CreateResponseMediaMessage(media);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        [HttpGet]
        [Route("api/media/{name}")]
        public HttpResponseMessage GetByName(string name)
        {
            try
            {
                var media = _service.GetByName(name) ?? new Media();
                return CreateResponseMediaMessage(media);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        
        [HttpPost]
        [Route("api/media")]
        public bool Post([FromBody]Media media)
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
        [Route("api/media")]
        public bool Delete([FromBody]int mediaId)
        {
            try
            {
                _service.Delete(mediaId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private HttpResponseMessage CreateResponseMediaMessage(Media media)
        {
            try
            {
                var response = new HttpResponseMessage
                {
                    Content = new StreamContent(new FileStream(media.MediaPath, FileMode.Open, FileAccess.Read))
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(media.MediaType);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
