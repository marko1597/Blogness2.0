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
    public class PostContentsLogic : IPostContentsLogic
    {
        private readonly IPostContentRepository _postContentRepository;

        public PostContentsLogic(IPostContentRepository postContentRepository)
        {
            _postContentRepository = postContentRepository;
        }

        public List<PostContent> GetByPostId(int postId)
        {
            var postContents = new List<PostContent>();
            try
            {
                var db = _postContentRepository.Find(a => a.PostId == postId, true).ToList();
                db.ForEach(a => postContents.Add(PostContentMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return postContents;
        }

        public PostContent Get(int postContentId)
        {
            try
            {
                var db = _postContentRepository.Find(a => a.PostContentId == postContentId, true).FirstOrDefault();

                if (db != null)
                {
                    return PostContentMapper.ToDto(db);
                }

                return new PostContent().GenerateError<PostContent>(
                    (int)Constants.Error.RecordNotFound,
                    string.Format("Cannot find post content with Id {0}", postContentId));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public PostContent Add(PostContent postContent)
        {
            try
            {
                return PostContentMapper.ToDto(_postContentRepository.Add(PostContentMapper.ToEntity(postContent)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int postContentId)
        {
            try
            {
                var db = _postContentRepository.Find(a => a.PostContentId == postContentId, false).FirstOrDefault();
                if (db == null) return false;

                _postContentRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
