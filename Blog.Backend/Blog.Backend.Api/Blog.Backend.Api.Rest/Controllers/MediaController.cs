using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Utils;
using Blog.Backend.Common.Web.Attributes;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    [AllowCrossSiteApi]
    public class MediaController : ApiController
    {
        private readonly IMedia _media;
        private readonly IUser _user;
        private readonly IImageHelper _imageHelper;
        private readonly string _mediaPath = ConfigurationManager.AppSettings.Get("MediaLocation");

        public MediaController(IMedia media, IUser user, IImageHelper imageHelper)
        {
            _media = media;
            _user = user;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        [Route("api/album/{albumId:int}/media")]
        public List<Media> GetByGroup(int albumId)
        {
            var media = new List<Media>();
            try
            {
                media = _media.GetByGroup(albumId) ?? new List<Media>();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                media = _media.GetByUser(userId) ?? new List<Media>();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return media;
        }

        [HttpGet]
        [Route("api/media/{mediaId:int}")]
        public HttpResponseMessage Get(int mediaId)
        {
            try
            {
                var media = _media.Get(mediaId) ?? new Media();
                return CreateResponseMediaMessage(media);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }

        [HttpGet]
        [Route("api/media/{name}")]
        public HttpResponseMessage GetByName(string name)
        {
            try
            {
                var media = _media.GetByName(name) ?? new Media();
                return CreateResponseMediaMessage(media);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }

        [HttpGet]
        [Route("api/media/{name}/thumb")]
        public HttpResponseMessage GetThumbnailByName(string name)
        {
            try
            {
                var media = _media.GetByName(name) ?? new Media();
                return CreateResponseMediaMessage(media);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("api/media")]
        public async Task<Media> Post([FromUri]string username, string album)
        {
            try
            {
                var user = _user.GetByUserName(username);
                var filename = string.Empty;
                var chunkName = string.Empty;

                var streamProvider = new MultipartFormDataStreamProvider(_mediaPath);
                await Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(
                    t =>
                    {
                        chunkName = streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault();
                        filename = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).FirstOrDefault();
                    });

                var resultMedia = _media.Add(user, album, filename, chunkName,
                    streamProvider.FileData[0].Headers.ContentType.ToString());

                return resultMedia;
            }
            catch
            {
                return new Media();
            }
        }

        [HttpDelete]
        [Route("api/media")]
        public bool Delete([FromBody]int mediaId)
        {
            try
            {
                _media.Delete(mediaId);
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
                    Content = new StreamContent(new FileStream(media.MediaPath + media.FileName, FileMode.Open, FileAccess.Read))
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(media.MediaType);

                return response;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }
    }
}
