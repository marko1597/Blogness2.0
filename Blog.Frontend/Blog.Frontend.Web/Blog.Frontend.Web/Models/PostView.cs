using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Frontend.Web.Models
{
    public class PostView
    {
        public Post Post { get; set; }
        public bool IsEdit { get; set; }
        public bool IsAdd { get; set; }
        public bool DisplayComments { get; set; }
    }
}