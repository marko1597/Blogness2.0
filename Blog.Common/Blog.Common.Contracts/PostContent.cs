using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class PostContent : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string PostContentTitle { get; set; }

        [DataMember]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string PostContentText { get; set; }

        [DataMember]
        public Media Media { get; set; }
    }
}
