﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class CommentLikesResource : BaseResource, ICommentLikesResource
    {
        public List<CommentLike> Get(int commentId)
        {
            using (var svc = new ServiceProxyHelper<ICommentLikesService>("CommentLikesService"))
            {
                return svc.Proxy.Get(commentId);
            }
        }

        public CommentLike Add(CommentLike commentLike)
        {
            using (var svc = new ServiceProxyHelper<ICommentLikesService>("CommentLikesService"))
            {
                return svc.Proxy.Add(commentLike);
            }
        }
    }
}