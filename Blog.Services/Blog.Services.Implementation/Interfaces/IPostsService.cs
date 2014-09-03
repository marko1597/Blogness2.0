using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IPostsService : IBaseService
    {
        [OperationContract]
        Post GetPost(int postId);

        [OperationContract]
        RelatedPosts GetRelatedPosts(int postId);

        [OperationContract]
        List<Post> GetPostsByTag(string tagName);

        [OperationContract]
        List<Post> GetMorePostsByTag(string tagName, int skip);

        [OperationContract]
        List<Post> GetPostsByUser(int userId);

        [OperationContract]
        List<Post> GetMorePostsByUser(int userId, int skip);

        [OperationContract]
        List<Post> GetPopularPosts(int postsCount);

        [OperationContract]
        List<Post> GetMorePopularPosts(int postsCount, int skip);

        [OperationContract]
        List<Post> GetRecentPosts(int postsCount);

        [OperationContract]
        List<Post> GetMoreRecentPosts(int postsCount, int skip);

        [OperationContract]
        Post AddPost(Post post);

        [OperationContract]
        Post UpdatePost(Post post);

        [OperationContract]
        bool DeletePost(int postId);
    }
}
