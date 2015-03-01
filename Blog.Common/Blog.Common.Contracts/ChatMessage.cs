using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class ChatMessage : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public User FromUser { get; set; }

        [DataMember]
        [Required]
        public User ToUser { get; set; }

        [DataMember]
        [Required]
        public string Text { get; set; }
    }
}
