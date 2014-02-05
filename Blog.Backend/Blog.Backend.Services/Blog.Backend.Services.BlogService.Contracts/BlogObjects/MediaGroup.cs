using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class MediaGroup
    {
        [DataMember]
        public int MediaGroupId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string MediaGroupName { get; set; }

        [DataMember]
        public bool IsUserDefault { get; set; }

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
