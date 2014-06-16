using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;
 
namespace Blog.Services.Implementation
{
    public class PostsService : IPosts
    {
        public Post GetPost(int postId)
        {
            return PostsFactory.GetInstance().CreateLogic().GetPost(postId);
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            return PostsFactory.GetInstance().CreateLogic().GetPostsByTag(tagName);
        }

        public List<Post> GetPostsByUser(int userId)
        {
            return PostsFactory.GetInstance().CreateLogic().GetPostsByUser(userId);
        }
        
        public List<Post> GetPopularPosts(int postsCount)
        {
            return PostsFactory.GetInstance().CreateLogic().GetPopularPosts(postsCount);
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            return PostsFactory.GetInstance().CreateLogic().GetRecentPosts(postsCount);
        }

        public List<Post> GetMorePosts(int postsCount, int skip)
        {
            return PostsFactory.GetInstance().CreateLogic().GetMorePosts(postsCount, skip);
        }

        public Post AddPost(Post post)
        {
            return PostsFactory.GetInstance().CreateLogic().AddPost(post);
        }

        public Post UpdatePost(Post post)
        {
            return PostsFactory.GetInstance().CreateLogic().UpdatePost(post);
        }

        public bool DeletePost(int postId)
        {
            return PostsFactory.GetInstance().CreateLogic().DeletePost(postId);
        }
    }
}
