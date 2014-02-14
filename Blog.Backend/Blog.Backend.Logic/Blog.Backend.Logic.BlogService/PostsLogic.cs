using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class PostsLogic
    {
        private readonly IPostResource _postResource;
        private readonly IPostTagResource _postTagResource;

        public PostsLogic(IPostResource postResource, IPostTagResource postTagResource)
        {
            _postResource = postResource;
            _postTagResource = postTagResource;
        }

        public Post GetPost(int postId)
        {
            var post = new Post();
            try
            {
                post = _postResource.Get(a => a.PostId == postId).FirstOrDefault();
                if (post != null)
                {
                    post.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(postId);
                    post.Comments = CommentsFactory.GetInstance().CreateCommentLikes().GetByPostId(postId);
                    post.User = UsersFactory.GetInstance().CreateUsers().Get(post.UserId);
                    post.PostLikes = PostLikesFactory.GetInstance().CreatePostLikes().Get(postId);
                }
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
                var firstOrDefault = TagsFactory.GetInstance().CreateTags().GetTagsByName(tagName).FirstOrDefault();
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
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                    a.Comments = CommentsFactory.GetInstance().CreateCommentLikes().GetByPostId(a.PostId);
                    a.User = UsersFactory.GetInstance().CreateUsers().Get(a.UserId);
                    a.PostLikes = PostLikesFactory.GetInstance().CreatePostLikes().Get(a.PostId);
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
