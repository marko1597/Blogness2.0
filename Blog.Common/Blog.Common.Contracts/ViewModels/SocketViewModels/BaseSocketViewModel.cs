using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    public class BaseSocketViewModel : BaseObject
    {
        [DataMember]
        [JsonProperty(PropertyName = "fn")]
        public string ClientFunction { get; set; }
    }
}
