using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Tag : BaseObject
    {
        [DataMember]
        public int TagId { get; set; }

        [DataMember]
        public string TagName { get; set; }
    }
}
