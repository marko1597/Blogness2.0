using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class CommentAdded : BaseSocketViewModel
    {
        [DataMember]
        public int? PostId { get; set; }

        [DataMember]
        public int? CommentId { get; set; }

        [DataMember]
        public Comment Comment { get; set; }
    }
}
