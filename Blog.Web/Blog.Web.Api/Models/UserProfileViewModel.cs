using System.Collections.Generic;
using System.Runtime.Serialization;
using Blog.Common.Contracts;

namespace Blog.Web.Api.Models
{
    [DataContract]
    public class UserProfileViewModel : User
    {
        [DataMember]
        public List<EducationGroup> EducationGroups { get; set; }
    }

    [DataContract]
    public class EducationGroup
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int EducationType { get; set; }

        [DataMember]
        public List<Education> Content { get; set; } 
    }
}