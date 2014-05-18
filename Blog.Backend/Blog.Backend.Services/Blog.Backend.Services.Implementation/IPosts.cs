using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IPosts
    {
        Post GetPost(int postId);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetPostsByUser(int userId);
        Post SavePost(Post post, bool isAdding);
        bool DeletePost(int postId);
    }
}
