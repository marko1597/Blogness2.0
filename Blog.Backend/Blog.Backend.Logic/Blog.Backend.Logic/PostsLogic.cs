using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.Utils;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class PostsLogic
    {
        private readonly IPostRepository _postRepository;

        public PostsLogic(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Post GetPost(int postId)
        {
            Post post;
            try
            {
                var db = _postRepository.Find(a => a.PostId == postId, null, "Tags,User,Comments,PostLikes").FirstOrDefault();
                post = PostMapper.ToDto(db);
                post.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(postId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return post;
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            var posts = new List<Post>();
            try
            {
                var tag = TagsFactory.GetInstance().CreateTags().GetTagsByName(tagName).FirstOrDefault();
                var db = _postRepository.Find(a => a.Tags.Contains(TagMapper.ToEntity(tag)), null, "Tags,User,Comments,PostLikes").ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a =>
                              {
                                  a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                              });
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetPostsByUser(int userId)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.Find(a => a.UserId == userId, null, "Tags,User,Comments,PostLikes").ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a =>
                {
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                });
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public Post UpdatePost(Post post)
        {
            try
            {
                var tPost = _postRepository.Edit(PostMapper.ToEntity(post));
                return PostMapper.ToDto(tPost);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Post AddPost(Post post)
        {
            try
            {
                foreach (var tag in post.Tags)
                {
                    tag.CreatedBy = post.User.UserId;
                    tag.CreatedDate = DateTime.UtcNow;
                    tag.ModifiedBy = post.User.UserId;
                    tag.ModifiedDate = DateTime.UtcNow;
                    tag.TagName = tag.TagName.ToLower();
                }

                foreach (var postContent in post.PostContents)
                {
                    postContent.CreatedBy = post.User.UserId;
                    postContent.CreatedDate = DateTime.UtcNow;
                    postContent.ModifiedBy = post.User.UserId;
                    postContent.ModifiedDate = DateTime.UtcNow;
                }

                post.CreatedBy = post.User.UserId;
                post.CreatedDate = DateTime.UtcNow;
                post.ModifiedBy = post.User.UserId;
                post.ModifiedDate = DateTime.UtcNow;

                var tPost = _postRepository.Add(PostMapper.ToEntity(post));
                return GetPost(tPost.PostId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool DeletePost(int postId)
        {
            try
            {
                var db = _postRepository.Find(p => p.PostId == postId, true).First();
                _postRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
