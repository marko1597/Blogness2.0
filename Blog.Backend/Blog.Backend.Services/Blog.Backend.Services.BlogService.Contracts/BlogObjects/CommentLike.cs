using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class CommentLike
    {
        [Key]
        [DataMember]
        public int CommentLikeId { get; set; }

        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public int UserId { get; set; }
        
        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }
    }
}
