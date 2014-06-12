using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Factory;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class PostContentsLogic
    {
        private readonly IPostContentRepository _postContentRepository;
        private readonly IMediaRepository _mediaRepository;

        public PostContentsLogic(IPostContentRepository postContentRepository, IMediaRepository mediaRepository)
        {
            _postContentRepository = postContentRepository;
            _mediaRepository = mediaRepository;
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
            PostContent postContent;
            try
            {
                var db = _postContentRepository.Find(a => a.PostContentId == postContentId, true).FirstOrDefault();
                postContent = PostContentMapper.ToDto(db);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return postContent;
        }

        public bool Add(PostContent postContent)
        {
            try
            {
                if (postContent.Media != null && postContent.Media.MediaId == 0)
                {
                    _mediaRepository.Add(MediaMapper.ToEntity(postContent.Media));
                }
                _postContentRepository.Add(PostContentMapper.ToEntity(postContent));

                return true;
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
                var db = _postContentRepository.Find(a => a.PostContentId == postContentId, true).FirstOrDefault();
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
