using System;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Education : BaseObject
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
    }
}
