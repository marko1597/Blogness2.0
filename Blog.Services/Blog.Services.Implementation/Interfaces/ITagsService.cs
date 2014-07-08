using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface ITagsService : IBaseService
    {
        [OperationContract]
        List<Tag> GetByPostId(int postId);

        [OperationContract]
        List<Tag> GetByName(string tagName);

        [OperationContract]
        Tag Add(Tag tag);
    }
}
