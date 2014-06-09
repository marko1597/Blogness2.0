using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class PostLikesUpdate
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public List<PostLike> PostLikes { get; set; }
    }
}
