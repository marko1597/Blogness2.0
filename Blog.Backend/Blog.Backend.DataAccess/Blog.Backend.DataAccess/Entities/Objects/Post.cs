using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.DataAccess.Entities.Objects
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string PostTitle { get; set; }
        public string PostMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<PostContent> PostContents { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
