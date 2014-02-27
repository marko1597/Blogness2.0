using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class Media
    {
        [DataMember]
        public int MediaId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string MediaType { get; set; }

        [DataMember]
        public int MediaGroupId { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string CustomName { get; set; }

        [DataMember]
        public string MediaUrl { get; set; }

        [DataMember]
        public string MediaPath { get; set; }

        [DataMember]
        public byte[] MediaContent { get; set; }

        [DataMember]
        public string ThumbnailUrl { get; set; }

        [DataMember]
        public string ThumbnailPath { get; set; }

        [DataMember]
        public byte[] ThumbnailContent { get; set; }

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
