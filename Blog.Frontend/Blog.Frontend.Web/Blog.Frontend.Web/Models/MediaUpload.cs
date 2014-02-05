using System.Web;

namespace Blog.Frontend.Web.Models
{
    public class MediaUpload
    {
        public HttpPostedFileBase MediaUploadFile { get; set; }
        public string CKEditorFuncNum { get; set; }
        public string CKEditor { get; set; }
        public string LanguageCode { get; set; }
    }
}