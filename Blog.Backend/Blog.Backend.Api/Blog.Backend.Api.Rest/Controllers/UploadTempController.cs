using System.Web;
using System.Web.Http;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class UploadTempController : ApiController
    {
        [HttpPost]
        [Route("api/uploadtemp")]
        public void Post(HttpPostedFileBase file)
        {
            var bytePostedFile = new byte[file.ContentLength];
            file.InputStream.Read(bytePostedFile, 0, file.ContentLength);
        }
    }
}
