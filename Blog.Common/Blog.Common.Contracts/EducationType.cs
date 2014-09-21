using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class EducationType : BaseObject
    {
        [DataMember]
        public int EducationTypeId { get; set; }

        [DataMember]
        [Display(Name = "Level")]
        public string EducationTypeName { get; set; }
    }
}
