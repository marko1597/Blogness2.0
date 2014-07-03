using System;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    
    [DataContract]
    public class PostContent : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public string PostContentTitle { get; set; }

        [DataMember]
        public string PostContentText { get; set; }

        [DataMember]
        public Media Media { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
