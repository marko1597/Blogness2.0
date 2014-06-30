using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class CommentLikesUpdate : BaseObject
    {
        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public List<CommentLike> CommentLikes { get; set; }
    }
}
