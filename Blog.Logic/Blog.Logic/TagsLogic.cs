using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.Utils;
using Blog.DataAccess.Database.Repository;
using Blog.Logic.Core.Factory;
using Blog.Logic.Core.Mapper;

namespace Blog.Logic.Core
{
    public class TagsLogic
    {
        private readonly ITagRepository _tagRepository;

        public TagsLogic(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public List<Tag> GetByPostId(int postId)
        {
            var tags = new List<Tag>();
            try
            {
                var post = PostsFactory.GetInstance().CreatePosts().GetPost(postId);
                var db = _tagRepository.Find(a => a.Posts.Contains(PostMapper.ToEntity(post))).ToList();
                db.ForEach(a => tags.Add(TagMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return tags;
        }

        public List<Tag> GetTagsByName(string tagName)
        {
            var tags = new List<Tag>();
            try
            {
                var db = _tagRepository.Find(a => a.TagName.Contains(tagName), true).ToList();
                db.ForEach(a => tags.Add(TagMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return tags;
        }

        public bool Add(Tag tag)
        {
            try
            {
                _tagRepository.Add(TagMapper.ToEntity(tag));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
