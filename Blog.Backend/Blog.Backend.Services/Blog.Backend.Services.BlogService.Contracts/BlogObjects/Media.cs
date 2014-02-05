using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
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
        public MediaGroup MediaGroup { get; set; }
        public int MediaGroupId { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string ExternalUrl { get; set; }

        [DataMember]
        public string MediaPath { get; set; }

        [DataMember]
        public byte[] MediaContent { get; set; }

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
