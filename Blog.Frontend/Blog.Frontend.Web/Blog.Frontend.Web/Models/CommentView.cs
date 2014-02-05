using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System.Collections.Generic;

namespace Blog.Frontend.Web.Models
{
    public class CommentView
    {
        public int? PostId { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsCommentReply { get; set; }
    }
}