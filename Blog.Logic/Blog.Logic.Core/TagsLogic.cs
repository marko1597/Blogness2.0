using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class TagsLogic : ITagsLogic
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;

        public TagsLogic(ITagRepository tagRepository, IPostRepository postRepository)
        {
            _tagRepository = tagRepository;
            _postRepository = postRepository;
        }

        public List<Tag> GetByPostId(int postId)
        {
            var tags = new List<Tag>();
            try
            {
                var post = _postRepository.Find(a => a.PostId == postId, null, "Tags").FirstOrDefault();

                if (post != null)
                {
                    var db = _tagRepository.Find(a => a.Posts.Contains(post), null, string.Empty).ToList();
                    db.ForEach(a => tags.Add(TagMapper.ToDto(a)));
                }
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

        public Tag Add(Tag tag)
        {
            try
            {
                var dbTags = _tagRepository.Find(a => a.TagName.ToLower() == tag.TagName, null, string.Empty).ToList();

                if (dbTags.Count == 0)
                {
                    var tTag = _tagRepository.Add(TagMapper.ToEntity(tag));
                    return TagMapper.ToDto(tTag);
                }

                return new Tag().GenerateError<Tag>((int) Constants.Error.ValidationError, "Record already exists");
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
