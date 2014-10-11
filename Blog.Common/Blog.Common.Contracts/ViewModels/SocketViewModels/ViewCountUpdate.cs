using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels.SocketViewModels
{
    [DataContract]
    public class ViewCountUpdate : BaseSocketViewModel
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public List<ViewCount> ViewCounts { get; set; }
    }
}
