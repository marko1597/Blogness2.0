using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string IdentityId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public DateTime BirthDate { get; set; }

        public int? PictureId { get; set; }

        public int? BackgroundId { get; set; }

        public virtual Address Address { get; set; }

        public int? UserPictureId { get; set; }

        public int? UserBackgroundId { get; set; }

        public virtual ICollection<Education> Education { get; set; }
        public virtual ICollection<Hobby> Hobbies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<ChatMessage> SentChatMessages { get; set; }
        public virtual ICollection<ChatMessage> ReceivedChatMessages { get; set; }
    }
}
