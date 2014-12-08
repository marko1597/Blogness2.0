using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Community : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Description { get; set; }

        [DataMember]
        public User Leader { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public List<User> Members { get; set; }

        [DataMember]
        public List<Post> Posts { get; set; }
    }
}
