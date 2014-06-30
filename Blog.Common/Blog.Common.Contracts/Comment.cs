using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Comment : BaseObject
    {
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
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }

        [DataMember]
        public string CommentLocation { get; set; }

        [DataMember]
        public List<CommentLike> CommentLikes { get; set; }
    }
}
