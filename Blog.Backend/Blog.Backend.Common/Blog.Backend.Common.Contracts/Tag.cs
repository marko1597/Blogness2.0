using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public int TagId { get; set; }

        [DataMember]
        public string TagName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
