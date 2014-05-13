using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class Media : BaseContract
    {
        [DataMember]
        public int MediaId { get; set; }

        [DataMember]
        public string MediaType { get; set; }

        [DataMember]
        public int AlbumId { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string CustomName { get; set; }

        [DataMember]
        public string MediaUrl { get; set; }
        public string MediaPath { get; set; }

        [DataMember]
        public byte[] MediaContent { get; set; }

        [DataMember]
        public string ThumbnailUrl { get; set; }
        public string ThumbnailPath { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
