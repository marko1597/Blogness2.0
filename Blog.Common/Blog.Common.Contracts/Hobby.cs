using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Hobby : BaseObject
    {
        [DataMember]
        public int HobbyId { get; set; }

        [DataMember]
        [Required]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Name")]
        public string HobbyName { get; set; }
    }
}
