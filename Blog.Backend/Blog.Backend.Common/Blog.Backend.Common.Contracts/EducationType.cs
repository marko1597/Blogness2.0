using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class EducationType
    {
        [DataMember]
        public int EducationTypeId { get; set; }

        [DataMember]
        public string EducationTypeName { get; set; }
    }
}
