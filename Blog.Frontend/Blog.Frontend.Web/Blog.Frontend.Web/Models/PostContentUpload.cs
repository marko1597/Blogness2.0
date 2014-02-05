using System.Web;

namespace Blog.Frontend.Web.Models
{
    public class PostContentUpload
    {
        public HttpPostedFileBase PostContentUploadFile { get; set; }
        public int PostId { get; set; }
    }
}