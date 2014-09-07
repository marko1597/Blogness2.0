using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class PostContentController : ApiController
    {
        private readonly IPostContentsResource _postContentsSvc;
        private readonly IErrorSignaler _errorSignaler;

        public PostContentController(IPostContentsResource postContentsSvc, IErrorSignaler errorSignaler)
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
                return new PostContent().GenerateError<PostContent>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPost]
        [Route("api/postcontent")]
        public PostContent Post([FromBody]PostContent postContent)
        {
            try
            {
                return _postContentsSvc.Add(postContent);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new PostContent().GenerateError<PostContent>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpDelete]
        [Route("api/postcontent/{postContentId}")]
        public bool Delete(int postContentId)
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
