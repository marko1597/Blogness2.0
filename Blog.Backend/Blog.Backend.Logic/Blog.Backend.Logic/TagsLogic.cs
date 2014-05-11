using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
