using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class Posts : IPosts
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

        public Post UpdatePost(Post post)
        {
            return PostsFactory.GetInstance().CreatePosts().UpdatePost(post);
        }

        public Post AddPost(Post post)
        {
            return PostsFactory.GetInstance().CreatePosts().AddPost(post);
        }

        public void DeletePost(int postId)
        {
            PostsFactory.GetInstance().CreatePosts().DeletePost(postId);
        }
    }
}
