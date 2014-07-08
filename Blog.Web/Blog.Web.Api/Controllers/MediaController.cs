using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class MediaController : ApiController
    {
        private readonly IMediaResource _media;
        private readonly IUsersResource _user;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IConfigurationHelper _configurationHelper;
        private readonly string _mediaPath = string.Empty;

        public MediaController(IMediaResource media, IUsersResource user, IErrorSignaler errorSignaler, IConfigurationHelper configurationHelper)
        {
            _media = media;
            _user = user;
            _errorSignaler = errorSignaler;
            _configurationHelper = configurationHelper;
            _mediaPath = _configurationHelper.GetAppSettings("MediaLocation");
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
                _errorSignaler.SignalFromCurrentContext(ex);
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
                _errorSignaler.SignalFromCurrentContext(ex);
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
                return CreateResponseMediaMessage(media, false);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }

        [HttpGet]
        [Route("api/media/{name}")]
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public HttpResponseMessage GetByName(string name)
        {
            try
            {
                var media = _media.GetByName(name) ?? new Media();
                return CreateResponseMediaMessage(media, false);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }

        [HttpGet]
        [Route("api/media/{name}/thumb")]
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public HttpResponseMessage GetThumbnailByName(string name)
        {
            try
            {
                var media = _media.GetByName(name) ?? new Media();
                return CreateResponseMediaMessage(media, true);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("api/media")]
        [BlogApiAuthorization]
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

                filename = filename.Substring(1, filename.Length - 2);
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

        private HttpResponseMessage CreateResponseMediaMessage(Media media, bool isThumb)
        {
            try
            {
                var response = new HttpResponseMessage
                {
                    Content = isThumb ?
                        new StreamContent(new FileStream(media.ThumbnailPath +
                            _configurationHelper.GetAppSettings("ThumbnailPrefix") +
                            Path.GetFileNameWithoutExtension(media.FileName) + ".jpg",
                            FileMode.Open, FileAccess.Read)) :
                        new StreamContent(new FileStream(media.MediaPath +
                            media.FileName, FileMode.Open, FileAccess.Read))
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(isThumb ? "image/jpeg" : media.MediaType);

                return response;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }
    }
}
