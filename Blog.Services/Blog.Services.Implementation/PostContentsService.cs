using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class PostContentsService : BaseService, IPostContentsService
    {
        private readonly IPostContentsLogic _postContentsLogic;

        public PostContentsService(IPostContentsLogic postsContentsLogic)
        {
            _postContentsLogic = postsContentsLogic;
        }

        public List<PostContent> GetByPostId(int postId)
        {
            return _postContentsLogic.GetByPostId(postId);
        }

        public PostContent Get(int postContentId)
        {
            return _postContentsLogic.Get(postContentId);
        }

        public PostContent Add(PostContent postImage)
        {
            return _postContentsLogic.Add(postImage);
        }

        public bool Delete(int postContentId)
        {
            return _postContentsLogic.Delete(postContentId);
        }
    }
}
