using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class PostLikesUpdate : BaseSocketViewModel
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public List<PostLike> PostLikes { get; set; }
    }
}
