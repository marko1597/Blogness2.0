using System;
using System.Collections.Generic;
using Radix.Backend.BlogService.Contracts;
using Radix.Backend.BlogService.Contracts.BlogObjects;
using Radix.Web.Common;

namespace Radix.Service.Helper
{
    public class BlogService : IBlogService
    {
        public User GetUser(int userId, string userName)
        {
            var user = new User();
            try
            {
                using(var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    user = client.Proxy.GetUser(userId, userName);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public List<Post> GetPosts(int userId, DateTime createdDate)
        {
            var posts = new List<Post>();
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    posts = client.Proxy.GetPosts(userId, createdDate);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return posts;
        }

        public List<Tag> GetTags(int postId)
        {
            var tags = new List<Tag>();
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    tags = client.Proxy.GetTags(postId);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tags;
        }

        public UserPosts GetUserPosts(int userId, DateTime createdDate)
        {
            var userPosts = new UserPosts();
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    userPosts = client.Proxy.GetUserPosts(userId, createdDate);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userPosts;
        }

        public User Login(string userName, string passWord)
        {
            var user = new User();
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    user = client.Proxy.Login(userName, passWord);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public User RegisterUser(string userName, string passWord, string emailAddress, string firstName, string lastName)
        {
            var user = new User();
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    user = client.Proxy.RegisterUser(userName, passWord, emailAddress, firstName, lastName);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }


        public void AddPost(Post post)
        {
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    client.Proxy.AddPost(post);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyPost(Post post)
        {
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    client.Proxy.ModifyPost(post);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeletePost(int postId)
        {
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    client.Proxy.DeletePost(postId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Logout(string userName)
        {
            bool loggedOut;
            try
            {
                using (var client = new ServiceProxy<IRadixBlogService>(Constants.RadixBlogService))
                {
                    loggedOut = client.Proxy.Logout(userName);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return loggedOut;
        }
    }
}
