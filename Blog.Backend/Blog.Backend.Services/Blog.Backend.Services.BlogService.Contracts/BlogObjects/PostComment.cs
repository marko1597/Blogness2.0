using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    public class PostComment
    {
        [Key]
        public int PostCommentId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
