using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.BlogService;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class PostContents
    {
        private readonly IPostContentResource _postContentResource;
        private readonly IMediaResource _mediaResource;

        public PostContents(IPostContentResource postContentResource, IMediaResource mediaResource)
        {
            _postContentResource = postContentResource;
            _mediaResource = mediaResource;
        }

        public List<PostContent> GetByPostId(int postId)
        {
            var postContents = new List<PostContent>();
            try
            {
                postContents = _postContentResource.Get(a => a.PostId == postId);
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
                postContent = _postContentResource.Get(a => a.PostContentId == postContentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return postContent;
        }

        public void Add(PostContent postContent)
        {
            try
            {
                if (postContent.Media != null && postContent.MediaId == 0)
                {
                    Utils.CreateDirectory(postContent.Media.MediaPath);
                    Utils.CreateThumbnailPath(postContent.Media.ThumbnailPath);

                    var media = _mediaResource.Add(postContent.Media);
                    postContent.MediaId = media.MediaId;
                }
                _postContentResource.Add(postContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int postContentId)
        {
            try
            {
                var postContent = _postContentResource.Get(a => a.PostContentId == postContentId).FirstOrDefault();
                _postContentResource.Delete(postContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
