﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class PostsResource : IPostsResource
    {
        public Post GetPost(int postId)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetPost(postId);
            }
        }

        public RelatedPosts GetRelatedPosts(int postId)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetRelatedPosts(postId);
            }
        }

        public List<Post> GetPostsByCommunity(int communityId, int threshold = 10, int skip = 10)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetPostsByCommunity(communityId, threshold, skip);
            }
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetPostsByTag(tagName);
            }
        }

        public List<Post> GetMorePostsByTag(string tagName, int skip)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetMorePostsByTag(tagName, skip);
            }
        }

        public List<Post> GetPostsByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetPostsByUser(userId);
            }
        }

        public List<Post> GetMorePostsByUser(int userId, int skip)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetMorePostsByUser(userId, skip);
            }
        }
        
        public List<Post> GetPopularPosts(int postsCount)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetPopularPosts(postsCount);
            }
        }

        public List<Post> GetMorePopularPosts(int postsCount, int skip)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetMorePopularPosts(postsCount, skip);
            }
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetRecentPosts(postsCount);
            }
        }

        public List<Post> GetMoreRecentPosts(int postsCount, int skip)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetMoreRecentPosts(postsCount, skip);
            }
        }

        public Post AddPost(Post post)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.AddPost(post);
            }
        }

        public Post UpdatePost(Post post)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.UpdatePost(post);
            }
        }

        public bool DeletePost(int postId)
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.DeletePost(postId);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IPostsService>("PostsService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
