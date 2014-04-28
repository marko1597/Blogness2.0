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
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
