using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class GlobalMessage : BaseSocketViewModel
    {
        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
