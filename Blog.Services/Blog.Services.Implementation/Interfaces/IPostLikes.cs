﻿using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IPostLikes
    {
        List<PostLike> Get(int postId);
        PostLike Add(PostLike postLike);
    }
}
