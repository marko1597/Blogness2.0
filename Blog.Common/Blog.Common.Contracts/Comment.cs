using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Comment : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? PostId { get; set; }

        [DataMember]
        public int? ParentCommentId { get; set; }

        [DataMember]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Message")]
        public string CommentMessage { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }

        [DataMember]
        public string CommentLocation { get; set; }

        [DataMember]
        public List<CommentLike> CommentLikes { get; set; }
    }
}
