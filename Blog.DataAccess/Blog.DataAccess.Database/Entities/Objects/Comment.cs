using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public virtual Post Post { get; set; }
        public int? PostId { get; set; }
        public virtual Comment ParentComment { get; set; }
        public int? ParentCommentId { get; set; }
        public string CommentMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string CommentLocation { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
    }
}
