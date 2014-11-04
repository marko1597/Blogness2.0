using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class ChatMessagesList : BaseObject
    {
        [DataMember]
        public List<ChatMessageListItem> ChatMessageListItems { get; set; } 
    }

    [DataContract]
    public class ChatMessageListItem
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public ChatMessage LastChatMessage { get; set; }
    }
}
