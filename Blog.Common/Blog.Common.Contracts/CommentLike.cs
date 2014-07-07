using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class CommentLike : BaseObject
    {
        [DataMember]
        public int CommentLikeId { get; set; }

        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public int UserId { get; set; }
    }
}
