using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class PostContentsLogic
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
                db.ForEach(a => PostContentMapper.ToDto(a));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return postContents;
        }

        public PostContent Get(int postContentId)
        {
            var postContent = new PostContent();
            try
            {
                var db = _postContentRepository.Find(a => a.PostContentId == postContentId, true).FirstOrDefault();
                postContent = PostContentMapper.ToDto(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return postContent;
        }

        public bool Add(PostContent postContent)
        {
            try
            {
                if (postContent.Media != null && postContent.Media.MediaId == 0)
                {
                    MediaFactory.GetInstance().CreateMedia().Add(postContent.Media);
                }
                _postContentRepository.Add(PostContentMapper.ToEntity(postContent));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
