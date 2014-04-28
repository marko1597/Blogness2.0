using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class CommentLike
    {
        [DataMember]
        public int CommentLikeId { get; set; }

        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
