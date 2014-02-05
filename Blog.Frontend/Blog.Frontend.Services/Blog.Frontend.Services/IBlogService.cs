using System.Net.Http;
using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Frontend.Services
{
    public interface IBlogService
    {
        #region Posts

        Post GetPost(int postId);
        List<Post> GetPostsByUser(int userId);
        List<Post> GetPostsByTag(string tagName);
        Post AddPost(Post post);
        Post AddNewPost(int userId);
        void ModifyPost(Post post);
        void DeletePost(int postId);

        #endregion

        #region Post Page

        UserPosts GetUserPosts(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetRecentPosts(int postsCount);

        #endregion

        #region Post Likes

        List<PostLike> GetPostLikes(int postId);
        void LikePost(PostLike postLike);

        #endregion

        #region Post Content

        List<PostContent> GetPostContents(int postId);
        PostContent GetPostContent(int postContentId);
        void AddPostContent(PostContent postContent);
        void DeletePostContent(int postContentId);

        #endregion

        #region Tags

        List<Tag> GetTags(int postId);

        #endregion

        #region Comments

        List<Comment> GetComments(int postId);
        List<Comment> GetCommentReplies(int commentId);
        List<CommentLike> GetCommentLikes(int commentId);
        void AddComment(Comment comment);
        void LikeComment(CommentLike commentLike);

        #endregion

        #region Users

        User GetUser(int userId, string username);
        User GetUserProfile(int userId);
        List<Education> GetUserEducation(int userId);
        List<Hobby> GetUserHobbies(int userId);
        Session IsLoggedIn(int userId);
        User Login(string userName, string password);
        bool Logout(string userName);
        User RegisterUser(string userName, string passWord, string emailAddress, string firstName, string lastName);

        #endregion

        #region Media

        List<Media> GetAllUserMedia(int userId);
        List<UserMediaGroup> GetAllUserMediaGrouped(int userId);
        Media GetUserMedia(int mediaId);
        HttpResponseMessage GetMediaItem(int mediaId);
        void AddMedia(Media media);
        void DeleteMedia(int mediaId);

        #endregion
    }
}
