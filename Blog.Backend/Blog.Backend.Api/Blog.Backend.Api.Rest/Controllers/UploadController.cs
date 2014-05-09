using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Backend.Api.Rest.Models;
using Blog.Backend.Common.Web.Attributes;

namespace Blog.Backend.Api.Rest.Controllers
{
    [AllowCrossSiteApi]
    public class UploadController : ApiController
    {
        private const string ServerUploadFolder = "C:\\Temp\\";

        [Route("api/upload")]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileUploadViewModel> Post(string username)
        {
            if (!Directory.Exists(ServerUploadFolder + username))
            {
                Directory.CreateDirectory(ServerUploadFolder + username);
            }

            var streamProvider = new MultipartFormDataStreamProvider(ServerUploadFolder + username);
            await Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(
                t =>
                {
                    var chunkName = streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault();
                    var fileName = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).FirstOrDefault();
                    if (fileName != null)
                    {
                        fileName = fileName.Substring(1, fileName.Length - 2);
                        if (chunkName != null)
                        {
                            File.Move(chunkName, ServerUploadFolder + username + "\\" + fileName);
                        }
                    }
                });

            return new FileUploadViewModel
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            };
        }
    }
}
