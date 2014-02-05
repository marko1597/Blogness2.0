using System.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using Blog.Frontend.Common;

namespace Blog.Frontend.Services
{
    public class BlogServiceApi : IBlogService
    {
        #region User related methods

        public User GetUser(int userId, string username)
        {
            var user = new User();
            try
            {
                user = JsonHelper.DeserializeJson<User>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/GetUser?userId=" + userId + "&username=" + username));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public User GetUserProfile(int userId)
        {
            var user = new User();
            try
            {
                user = JsonHelper.DeserializeJson<User>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/GetUserProfile?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public List<Education> GetUserEducation(int userId)
        {
            var education = new List<Education>();
            try
            {
                education = JsonHelper.DeserializeJson<List<Education>>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/GetUserEducation?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return education;
        }

        public List<Hobby> GetUserHobbies(int userId)
        {
            var hobbies = new List<Hobby>();
            try
            {
                hobbies = JsonHelper.DeserializeJson<List<Hobby>>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/GetUserHobbies?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return hobbies;
        }

        public Session IsLoggedIn(int userId)
        {
            var session = new Session();
            try
            {
                session = JsonHelper.DeserializeJson<Session>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/IsLoggedIn?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return session;
        }

        public User Login(string userName, string passWord)
        {
            var user = new User();
            try
            {
                user = JsonHelper.DeserializeJson<User>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/Login?username=" + userName + "&password=" + passWord));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public bool Logout(string userName)
        {
            bool loggedOut = false;
            try
            {
                loggedOut = JsonHelper.DeserializeJson<bool>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/Logout?username=" + userName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return loggedOut;
        }

        public User RegisterUser(string userName, string passWord, string emailAddress, string firstName, string lastName)
        {
            var user = new User();
            try
            {
                user = JsonHelper.DeserializeJson<User>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/user/RegisterUser?username=" + userName + "&password=" + passWord + "&emailAddress=" + emailAddress + "&firstName=" + firstName + "&lastName=" + lastName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        #endregion

        #region Post related methods

        public Post AddPost(Post post)
        {
            var newPost = new Post();
            try
            {
                JsonHelper.SerializeJson(post);
                newPost = JsonHelper.DeserializeJson<Post>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"]).Post("api/post/Add", post));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return newPost;
        }

        public Post AddNewPost(int userId)
        {
            var post = new Post();
            try
            {
                post = JsonHelper.DeserializeJson<Post>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/post/AddNew?userId=" + userId));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        public void ModifyPost(Post post)
        {
            try
            {
                JsonHelper.SerializeJson(post);
                JsonHelper.DeserializeJson<Post>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"]).Post("api/post/Modify", post));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeletePost(int postId)
        {
            try
            {
                JsonHelper.DeserializeJson<int>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"]).Get("api/post/Delete?postId=" + postId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Post GetPost(int postId)
        {
            var post = new Post();
            try
            {
                post = JsonHelper.DeserializeJson<Post>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/post/GetPost?postId=" + postId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        public List<Post> GetPostsByUser(int userId)
        {
            var posts = new List<Post>();
            try
            {
                posts = JsonHelper.DeserializeJson<List<Post>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/post/GetPostsByUser?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            var posts = new List<Post>();
            try
            {
                posts = JsonHelper.DeserializeJson<List<Post>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/post/GetPostsByTag?tagName=" + tagName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        #endregion

        #region Post page

        public UserPosts GetUserPosts(int userId)
        {
            var userPosts = new UserPosts();
            try
            {
                userPosts = JsonHelper.DeserializeJson<UserPosts>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/postpage/GetUserPosts?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userPosts;
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                posts = JsonHelper.DeserializeJson<List<Post>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/postpage/GetPopularPosts?postsCount=" + postsCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                posts = JsonHelper.DeserializeJson<List<Post>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/postpage/GetRecentPosts?postsCount=" + postsCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        #endregion

        #region Post Likes

        public void LikePost(PostLike postLike)
        {
            try
            {
                JsonHelper.SerializeJson(postLike);
                JsonHelper.DeserializeJson<PostLike>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"]).Post("api/postlike/LikePost", postLike));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<PostLike> GetPostLikes(int postId)
        {
            var postLikes = new List<PostLike>();
            try
            {
                postLikes = JsonHelper.DeserializeJson<List<PostLike>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/postlike/GetLikes?postId=" + postId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return postLikes;
        }

        #endregion

        #region Post Contents

        public List<PostContent> GetPostContents(int postId)
        {
            var contents = new List<PostContent>();
            try
            {
                contents = JsonHelper.DeserializeJson<List<PostContent>>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/postcontent/GetList?postId=" + postId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return contents; 
        }

        public PostContent GetPostContent(int postContentId)
        {
            var content = new PostContent();
            try
            {
                content = JsonHelper.DeserializeJson<PostContent>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/postcontent/Get?postContentId=" + postContentId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return content;
        }

        public void AddPostContent(PostContent postContent)
        {
            try
            {
                JsonHelper.SerializeJson(postContent);
                JsonHelper.DeserializeJson<Post>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Post("api/postcontent/Add", postContent));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeletePostContent(int postContentId)
        {
            try
            {
                JsonHelper.DeserializeJson<int>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                        .Get("api/postcontent/Delete?postContentId=" + postContentId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Comment related methods

        public List<Comment> GetComments(int postId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = JsonHelper.DeserializeJson<List<Comment>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/comment/GetComments?postId=" + postId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public List<Comment> GetCommentReplies(int commentId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = JsonHelper.DeserializeJson<List<Comment>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/comment/GetCommentReplies?commentId=" + commentId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public List<CommentLike> GetCommentLikes(int commentId)
        {
            var commentLikes = new List<CommentLike>();
            try
            {
                commentLikes = JsonHelper.DeserializeJson<List<CommentLike>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/comment/GetLikes?commentId=" + commentId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return commentLikes;
        }

        public void AddComment(Comment comment)
        {
            JsonHelper.SerializeJson(comment);
            JsonHelper.DeserializeJson<Comment>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"]).Post("api/Comment/Add", comment));
        }

        public void LikeComment(CommentLike commentLike)
        {
            try
            {
                JsonHelper.SerializeJson(commentLike);
                JsonHelper.DeserializeJson<CommentLike>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"]).Post("api/comment/LikeComment", commentLike));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Tag related methods

        public List<Tag> GetTags(int postId)
        {
            var tags = new List<Tag>();
            try
            {
                tags = JsonHelper.DeserializeJson<List<Tag>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/tag/GetTags?postId=" + postId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tags;
        }

        #endregion

        #region Media

        public HttpResponseMessage GetMediaItem(int mediaId)
        {
            var media = new HttpResponseMessage();
            try
            {
                media = JsonHelper.DeserializeJson<HttpResponseMessage>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/media/GetMediaItem?mediaId=" + mediaId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public List<UserMediaGroup> GetAllUserMediaGrouped(int userId)
        {
            var userMedia = new List<UserMediaGroup>();
            try
            {
                userMedia = JsonHelper.DeserializeJson<List<UserMediaGroup>>(
                   new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/media/GetListGrouped?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userMedia;
        }

        public List<Media> GetAllUserMedia(int userId)
        {
            var media = new List<Media>();
            try
            {
                media = JsonHelper.DeserializeJson<List<Media>>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/media/GetList?userId=" + userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public Media GetUserMedia(int mediaId)
        {
            var media = new Media();
            try
            {
                media = JsonHelper.DeserializeJson<Media>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Get("api/media/Get?mediaId=" + mediaId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public void AddMedia(Media media)
        {
            try
            {
                JsonHelper.SerializeJson(media);
                JsonHelper.DeserializeJson<Post>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                    .Post("api/media/Add", media));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteMedia(int mediaId)
        {
            try
            {
                JsonHelper.DeserializeJson<int>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApiEndpoint"])
                        .Get("api/media/Delete?MediaId=" + mediaId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
