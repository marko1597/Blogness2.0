using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public int TagId { get; set; }

        [DataMember]
        public string TagName { get; set; }

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
