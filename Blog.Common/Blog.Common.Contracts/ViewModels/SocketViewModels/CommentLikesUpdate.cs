using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class CommentLikesUpdate : BaseSocketViewModel
    {
        [DataMember]
        public int? PostId { get; set; }

        [DataMember]
        public int? CommentId { get; set; }

        [DataMember]
        public List<CommentLike> CommentLikes { get; set; }
    }
}
