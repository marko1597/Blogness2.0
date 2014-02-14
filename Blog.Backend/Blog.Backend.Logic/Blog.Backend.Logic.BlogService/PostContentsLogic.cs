using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.BlogService;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class PostContentsLogic
    {
        private readonly IPostContentResource _postContentResource;

        public PostContentsLogic(IPostContentResource postContentResource)
        {
            _postContentResource = postContentResource;
        }

        public List<PostContent> GetByPostId(int postId)
        {
            var postContents = new List<PostContent>();
            try
            {
                postContents = _postContentResource.Get(a => a.PostId == postId);
                postContents.ForEach(a =>
                {
                    a.Media = MediaFactory.GetInstance().CreateMedia().Get(a.MediaId);
                    if (a.Media != null)
                    {
                        a.Media.MediaContent = null;
                        a.Media.ThumbnailContent = null;
                    }
                });
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
                if (postContent != null)
                {
                    postContent.Media = MediaFactory.GetInstance().CreateMedia().Get(postContent.MediaId);
                    if (postContent.Media != null) postContent.Media.MediaContent = null;
                }
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

                    var media = MediaFactory.GetInstance().CreateMedia().Add(postContent.Media);
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
