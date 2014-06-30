using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class LoggedUser : BaseObject
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Session Session { get; set; }
    }
}
