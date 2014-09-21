using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    [KnownType(typeof(PostLike))]
    [KnownType(typeof(PostContent))]
    public class Post : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Title")]
        public string PostTitle { get; set; }

        [DataMember]
        [Display(Name = "Message")]
        public string PostMessage { get; set; }

        [DataMember]
        public List<PostContent> PostContents { get; set; }

        [DataMember]
        public List<Tag> Tags { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }

        [DataMember]
        public List<PostLike> PostLikes { get; set; }
    }
}
