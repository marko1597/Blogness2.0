using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class CommentLike
    {
        [Key]
        public int CommentLikeId { get; set; }
        public virtual Comment Comment { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
