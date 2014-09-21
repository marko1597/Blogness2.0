using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Education : BaseObject
    {
        [DataMember]
        public int EducationId { get; set; }

        [DataMember]
        [Required]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        public EducationType EducationType { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        [Display(Name = "Year Attended")]
        public DateTime? YearAttended { get; set; }

        [DataMember]
        [Display(Name = "Year Graduated")]
        public DateTime? YearGraduated { get; set; }

        [DataMember]
        public string Course { get; set; }
    }
}
