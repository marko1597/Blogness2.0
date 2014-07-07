using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Album : BaseObject
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
    }
}
