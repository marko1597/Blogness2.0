using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Hobby : BaseObject
    {
        [DataMember]
        public int HobbyId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string HobbyName { get; set; }
    }
}
