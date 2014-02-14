using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IPosts
    {
        Post GetPost(int postId);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetPostsByUser(int userId);
        Post UpdatePost(Post post);
        Post AddPost(Post post);
        void DeletePost(int postId);
    }
}
