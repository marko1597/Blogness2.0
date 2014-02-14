using System.Runtime.Serialization;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts.ViewModels
{
    [DataContract]
    public class LoggedUser
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Session Session { get; set; }
    }
}
