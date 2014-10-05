using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class PostComments : BaseSocketViewModel
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }
    }
}
