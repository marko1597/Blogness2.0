using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class CommentAdded : BaseObject
    {
        [DataMember]
        public int? PostId { get; set; }

        [DataMember]
        public Comment Comment { get; set; }
    }
}
