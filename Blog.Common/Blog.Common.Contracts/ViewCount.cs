using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class ViewCount : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public int? UserId { get; set; }
    }
}
