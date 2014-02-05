using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class Comment
    {
        [Key]
        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public int? PostId { get; set; }

        [DataMember]
        public int? ParentCommentId { get; set; }

        [DataMember]
        public string CommentMessage { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }

        [DataMember]
        public string CommentLocation { get; set; }

        [DataMember]
        public List<CommentLike> CommentLikes { get; set; }

        public int UserId { get; set; }
    }
}
