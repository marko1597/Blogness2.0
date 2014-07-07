using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class PostLike : BaseObject
    {
        [DataMember]
        public int PostLikeId { get; set; }

        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public int UserId { get; set; }
    }
}
