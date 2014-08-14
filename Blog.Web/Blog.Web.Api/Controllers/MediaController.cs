using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
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
                media.ForEach(a =>
                {
                    a.MediaPath = null;
                    a.ThumbnailPath = null;
                });
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
                media.ForEach(a =>
                {
                    a.MediaPath = null;
                    a.ThumbnailPath = null;
                });
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

        [HttpGet]
        [Route("api/media/video")]
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public HttpResponseMessage GetVideo()
        {
            try
            {
                var media = new Media
                            {
                                Id = 1,
                                AlbumId = 1,
                                CreatedBy = 1,
                                CustomName = Guid.NewGuid().ToString(),
                                FileName = "foo.webm",
                                MediaType = "video/webm",
                                MediaPath = @"C:\Temp\",
                                ThumbnailPath = @"C:\Temp\",
                                CreatedDate = DateTime.Now,
                                ModifiedBy = 1,
                                ModifiedDate = DateTime.Now
                            };

                return CreateResponseMediaMessage(media, false);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }

        [HttpPost, Authorize]
        [Route("api/media")]
        public async Task<Media> Post([FromUri]string username, string album)
        {
            try
            {
                var user = _user.GetByUserName(username);
                if (user == null || user.Error != null) throw new Exception("User not specified"); 

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

        [HttpPost, Authorize]
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

        [Route("api/media/defaultprofilepicture")]
        public HttpResponseMessage GetDefaultProfilePicture()
        {
            try
            {
                var mediaPath = _configurationHelper.GetAppSettings("MediaLocation") +
                               "default-profile-picture.png";
                var mediaType = new MediaTypeHeaderValue("image/png");
                var tMedia = new MediaStream(mediaPath);

                var response = Request.CreateResponse();
                response.Content = new PushStreamContent(
                    (Action<Stream, HttpContent, TransportContext>)tMedia.WriteToStream, mediaType);

                return response;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return null;
        }

        [Route("api/media/defaultbackgroundpicture")]
        public HttpResponseMessage GetDefaultBackgroundPicture()
        {
            try
            {
                var mediaPath = _configurationHelper.GetAppSettings("MediaLocation") +
                               "default-background-picture.jpg";
                var mediaType = new MediaTypeHeaderValue("image/jpg");
                var tMedia = new MediaStream(mediaPath);

                var response = Request.CreateResponse();
                response.Content = new PushStreamContent(
                    (Action<Stream, HttpContent, TransportContext>)tMedia.WriteToStream, mediaType);

                return response;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return null;
        }

        private HttpResponseMessage CreateResponseMediaMessage(Media media, bool isThumb)
        {
            try
            {
                string mediaPath;
                MediaTypeHeaderValue mediaType;

                if (isThumb)
                {
                    if (IsVideo(media.MediaType))
                    {
                        mediaPath = _configurationHelper.GetAppSettings("MediaLocation") +
                                    "default_vid_thumbnail.jpg";
                    }
                    else
                    {
                        mediaPath = media.ThumbnailPath + _configurationHelper.GetAppSettings("ThumbnailPrefix") + 
                            Path.GetFileNameWithoutExtension(media.FileName) + ".jpg";
                    }
                    mediaType = new MediaTypeHeaderValue("image/jpg");
                }
                else
                {
                    mediaPath = media.MediaPath + media.FileName;
                    mediaType = new MediaTypeHeaderValue(media.MediaType);
                }

                var tMedia = new MediaStream(mediaPath);

                var response = Request.CreateResponse();
                response.Content = new PushStreamContent(
                    (Action<Stream, HttpContent, TransportContext>)tMedia.WriteToStream, mediaType);

                return response;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }

        private static bool IsVideo(string mimeType)
        {
            var supportedMedia = new List<string>
            {
                "video/avi",
                "video/quicktime",
                "video/mpeg",
                "video/mp4",
                "video/x-flv",
                "video/webm"
            };

            return supportedMedia.Contains(mimeType);
        }
    }
}
