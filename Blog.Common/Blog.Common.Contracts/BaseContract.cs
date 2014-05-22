using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class BaseContract
    {
        [DataMember]
        public Error Error { get; set; }
    }
}
