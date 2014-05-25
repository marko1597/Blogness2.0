using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IPosts
    {
        Post GetPost(int postId);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetPostsByUser(int userId);
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        bool DeletePost(int postId);
    }
}
