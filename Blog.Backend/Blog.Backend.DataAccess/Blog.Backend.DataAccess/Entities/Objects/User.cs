using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.DataAccess.Entities.Objects
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Education> Education { get; set; }
        public virtual ICollection<Hobby> Hobbies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<MediaGroup> MediaGroups { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
