using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace BlogApi.Controllers
{
    public class MediaController : ApiController
    {
        private readonly IBlogService _service;

        public MediaController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetList")]
        public List<Media> GetList(int userId)
        {
            var media = new List<Media>();
            try
            {
                media = _service.GetAllUserMedia(userId) ?? new List<Media>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetListGrouped")]
        public List<UserMediaGroup> GetListGrouped(int userId)
        {
            var media = new List<UserMediaGroup>();
            try
            {
                media = _service.GetAllUserMediaGrouped(userId) ?? new List<UserMediaGroup>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetMediaItem")]
        public HttpResponseMessage GetMediaItem(int mediaId)
        {
            var media = _service.GetUserMedia(mediaId);

            var response = new HttpResponseMessage
                               {
                                   Content = new StreamContent(new FileStream(media.MediaPath, FileMode.Open, FileAccess.Read))
                               };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(media.MediaType);

            return response;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Get")]
        public Media Get(int mediaId)
        {
            try
            {
                return _service.GetUserMedia(mediaId) ?? new Media();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Add")]
        public bool Add(Media media)
        {
            try
            {
                _service.AddMedia(media);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Delete")]
        public bool Delete(int mediaId)
        {
            try
            {
                _service.DeleteMedia(mediaId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
