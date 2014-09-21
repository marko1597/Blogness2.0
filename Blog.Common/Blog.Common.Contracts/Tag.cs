using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Tag : BaseObject
    {
        [DataMember]
        public int TagId { get; set; }

        [DataMember]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string TagName { get; set; }
    }
}
