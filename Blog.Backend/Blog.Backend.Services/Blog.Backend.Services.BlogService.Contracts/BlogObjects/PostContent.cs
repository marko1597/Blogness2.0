using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    
    [DataContract]
    public class PostContent
    {
        [DataMember]
        public int PostContentId { get; set; }

        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public Media Media { get; set; }

        [DataMember]
        public int MediaId { get; set; }

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
