using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.DataAccess.Entities.Objects
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual EducationType EducationType { get; set; }
        public int EducationTypeId { get; set; }
        public string SchoolName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? YearAttended { get; set; }
        public DateTime? YearGraduated { get; set; }
        public string Course { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
