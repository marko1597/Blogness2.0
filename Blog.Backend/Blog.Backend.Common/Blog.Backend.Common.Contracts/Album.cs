using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class Album
    {
        [DataMember]
        public int AlbumId { get; set; }

        [DataMember]
        public string AlbumName { get; set; }

        [DataMember]
        public List<Media> Media { get; set; }

        [DataMember]
        public User User { get; set; }

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
