using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
{
    public class PostsService : IPosts
    {
        public Post GetPost(int postId)
        {
            return PostsFactory.GetInstance().CreatePosts().GetPost(postId);
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            return PostsFactory.GetInstance().CreatePosts().GetPostsByTag(tagName);
        }

        public List<Post> GetPostsByUser(int userId)
        {
            return PostsFactory.GetInstance().CreatePosts().GetPostsByUser(userId);
        }

        public Post AddPost(Post post)
        {
            return PostsFactory.GetInstance().CreatePosts().AddPost(post);
        }

        public Post UpdatePost(Post post)
        {
            return PostsFactory.GetInstance().CreatePosts().UpdatePost(post);
        }

        public bool DeletePost(int postId)
        {
            return PostsFactory.GetInstance().CreatePosts().DeletePost(postId);
        }
    }
}
