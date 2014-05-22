using System;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Education : BaseContract
    {
        [DataMember]
        public int EducationId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public EducationType EducationType { get; set; }

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
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
