using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    [KnownType(typeof(PostLike))]
    [KnownType(typeof(PostContent))]
    public class Post
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public string PostTitle { get; set; }

        [DataMember]
        public string PostMessage { get; set; }

        [DataMember]
        public List<PostContent> PostContents { get; set; }

        [DataMember]
        public List<Tag> Tags { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }

        [DataMember]
        public List<PostLike> PostLikes { get; set; }

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
