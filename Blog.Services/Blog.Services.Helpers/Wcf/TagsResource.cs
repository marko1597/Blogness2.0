using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class TagsResource : BaseResource, ITagsResource
    {
        public List<Tag> GetByPostId(int postId)
        {
            using (var svc = new ServiceProxyHelper<ITagsService>("TagsService"))
            {
                return svc.Proxy.GetByPostId(postId);
            }
        }

        public List<Tag> GetByName(string tagName)
        {
            using (var svc = new ServiceProxyHelper<ITagsService>("TagsService"))
            {
                return svc.Proxy.GetByName(tagName);
            }
        }

        public Tag Add(Tag tag)
        {
            using (var svc = new ServiceProxyHelper<ITagsService>("TagsService"))
            {
                return svc.Proxy.Add(tag);
            }
        }
    }
}
