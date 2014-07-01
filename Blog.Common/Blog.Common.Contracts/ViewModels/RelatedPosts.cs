using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class RelatedPosts : BaseObject
    {
        [DataMember]
        public List<Post> PostsByUser { get; set; }

        [DataMember]
        public List<Post> PostsByTags { get; set; }

        [DataMember]
        public List<Post> PostsByTitle { get; set; }
    }
}
