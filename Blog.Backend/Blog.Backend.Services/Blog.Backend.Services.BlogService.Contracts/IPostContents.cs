using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IPostContents
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        void Add(PostContent postImage);
        void Delete(int postContentId);
    }
}
