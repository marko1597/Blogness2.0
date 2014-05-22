using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class EducationType : BaseContract
    {
        [DataMember]
        public int EducationTypeId { get; set; }

        [DataMember]
        public string EducationTypeName { get; set; }
    }
}
