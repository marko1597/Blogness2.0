using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class Education
    {
        [Key]
        [DataMember]
        public int EducationId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public EducationType EducationType { get; set; }
        public int EducationTypeId { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public DateTime? YearAttended { get; set; }

        [DataMember]
        public DateTime? YearGraduated { get; set; }

        [DataMember]
        public string Course { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }
    }
}
