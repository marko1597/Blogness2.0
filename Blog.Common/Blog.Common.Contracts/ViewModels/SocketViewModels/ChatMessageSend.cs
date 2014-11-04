using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class ChatMessageSend : BaseSocketViewModel
    {
        [DataMember]
        public int FromUserId { get; set; }

        [DataMember]
        public int ToUserId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
