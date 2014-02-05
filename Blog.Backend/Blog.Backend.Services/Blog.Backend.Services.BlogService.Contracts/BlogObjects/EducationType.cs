using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class EducationType
    {
        [Key]
        [DataMember]
        public int EducationTypeId { get; set; }

        [DataMember]
        public string EducationTypeName { get; set; }
    }
}
