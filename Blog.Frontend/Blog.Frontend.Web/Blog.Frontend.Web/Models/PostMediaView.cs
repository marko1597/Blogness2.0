using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Frontend.Web.Models
{
    public class PostMediaView
    {
        public PostContent PostContent { get; set; }
        public bool AllowDelete { get; set; }
    }
}