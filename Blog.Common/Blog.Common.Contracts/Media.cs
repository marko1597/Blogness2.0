using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Media : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

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
    }
}
