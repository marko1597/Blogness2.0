using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class PostContentController : ApiController
    {
        private readonly IPostContents _postContentsSvc;
        private readonly IErrorSignaler _errorSignaler;

        public PostContentController(IPostContents postContentsSvc, IErrorSignaler errorSignaler)
        {
            _postContentsSvc = postContentsSvc;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/posts/{postId:int}/contents")]
        public List<PostContent> GetList(int postId)
        {
            var postContents = new List<PostContent>();
            try
            {
                postContents = _postContentsSvc.GetByPostId(postId) ?? new List<PostContent>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return postContents;
        }

        [HttpGet]
        [Route("api/postcontent/{postContentId:int}")]
        public PostContent Get(int postContentId)
        {
            try
            {
                return _postContentsSvc.Get(postContentId) ?? new PostContent();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("api/postcontent")]
        public bool Post([FromBody]PostContent postContent)
        {
            try
            {
                _postContentsSvc.Add(postContent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("api/postcontent")]
        public bool Delete([FromBody]int postContentId)
        {
            try
            {
                _postContentsSvc.Delete(postContentId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
