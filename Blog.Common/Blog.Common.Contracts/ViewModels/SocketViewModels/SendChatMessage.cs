using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class SendChatMessage : BaseSocketViewModel
    {
        [DataMember]
        public int RecipientUserId { get; set; }

        [DataMember]
        public ChatMessage ChatMessage { get; set; }
    }
}
