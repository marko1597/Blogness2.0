using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.Utils;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Factory;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
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
                var db = _postRepository.Find(a => a.PostId == postId, null, "Tags,User,PostLikes").FirstOrDefault();
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

        public Post AddPost(Post post)
        {
            try
            {
                post.Tags = PrepareTags(post.Tags, post.User.UserId);
                post.PostContents = PreparePostContents(post.PostContents, post.User.UserId, post.PostId);
                post.CreatedBy = post.User.UserId;
                post.CreatedDate = DateTime.Now;
                post.ModifiedBy = post.User.UserId;
                post.ModifiedDate = DateTime.Now;

                var tPost = _postRepository.Add(PostMapper.ToEntity(post));
                return GetPost(tPost.PostId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
        
        public Post UpdatePost(Post post)
        {
            try
            {
                post.Tags = PrepareTags(post.Tags, post.User.UserId);
                post.PostContents = PreparePostContents(post.PostContents, post.User.UserId, post.PostId);
                post.ModifiedDate = DateTime.Now;

                var tPost = _postRepository.Edit(PostMapper.ToEntity(post));
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

        private List<Tag> PrepareTags(IEnumerable<Tag> tags, int userId)
        {
            var enumerable = tags as Tag[] ?? tags.ToArray();
            foreach (var tag in enumerable)
            {
                tag.CreatedBy = userId;
                tag.CreatedDate = DateTime.Now;
                tag.ModifiedBy = userId;
                tag.ModifiedDate = DateTime.Now;
                tag.TagName = tag.TagName.ToLower();
            }

            return enumerable.ToList();
        }

        private List<PostContent> PreparePostContents(IEnumerable<PostContent> contents, int userId, int postId)
        {
            var postContents = contents as PostContent[] ?? contents.ToArray();
            foreach (var postContent in postContents)
            {
                postContent.CreatedBy = userId;
                postContent.CreatedDate = DateTime.Now;
                postContent.ModifiedBy = userId;
                postContent.ModifiedDate = DateTime.Now;
                postContent.PostId = postId;
            }

            return postContents.ToList();
        }
    }
}
