using System.Runtime.Serialization;
using Newtonsoft.Json;

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

        [DataMember, JsonIgnore]
        public string CustomName { get; set; }

        [DataMember]
        public string MediaUrl { get; set; }

        [DataMember, JsonIgnore]
        public string MediaPath { get; set; }

        [DataMember, JsonIgnore]
        public byte[] MediaContent { get; set; }

        [DataMember]
        public string ThumbnailUrl { get; set; }

        [DataMember, JsonIgnore]
        public string ThumbnailPath { get; set; }
    }
}
