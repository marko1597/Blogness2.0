using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class Posts
    {
        private readonly IPostResource _postResource;
        private readonly IPostTagResource _postTagResource;
        private readonly IPostContentResource _postContentResource;
        private readonly ITagResource _tagResource;

        public Posts(IPostResource postResource, IPostTagResource postTagResource, IPostContentResource postContentResource, ITagResource tagResource)
        {
            _postResource = postResource;
            _postTagResource = postTagResource;
            _postContentResource = postContentResource;
            _tagResource = tagResource;
        }

        public Post GetPost(int postId)
        {
            var post = new Post();
            try
            {
                post = _postResource.Get(a => a.PostId == postId).FirstOrDefault();
                if (post != null) post.PostContents = _postContentResource.Get(a => a.PostId == postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            var posts = new List<Post>();
            try
            {
                var firstOrDefault = _tagResource.Get(t => t.TagName == tagName).FirstOrDefault();
                var postIds = _postTagResource.Get(a => firstOrDefault != null && a.TagId == firstOrDefault.TagId).Select(a => a.PostId);
                posts = _postResource.Get(a => postIds.Contains(a.PostId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        public List<Post> GetPostsByUser(int userId)
        {
            var posts = new List<Post>();
            try
            {
                posts = _postResource.Get(a => a.UserId == userId).ToList();
                posts.ForEach(a =>
                {
                    a.PostContents = _postContentResource.Get(b => b.PostId == a.PostId);
                    a.PostContents.ForEach(b => { b.Media = null; });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        public Post UpdatePost(Post post)
        {
            try
            {
                post.UserId = post.User.UserId;
                return _postResource.Update(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Post AddPost(Post post)
        {
            try
            {
                post.UserId = post.User.UserId;
                return _postResource.Add(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void DeletePost(int postId)
        {
            try
            {
                var post = _postResource.Get(p => p.PostId == postId).First();
                _postResource.Delete(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
