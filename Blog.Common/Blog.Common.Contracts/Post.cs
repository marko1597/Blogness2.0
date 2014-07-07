using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    [KnownType(typeof(PostLike))]
    [KnownType(typeof(PostContent))]
    public class Post : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

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
    }
}
