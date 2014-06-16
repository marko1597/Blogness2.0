using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Factory;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class PostsPageLogic
    {
        private readonly IPostRepository _postRepository;

        public PostsPageLogic(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        
    }
}
