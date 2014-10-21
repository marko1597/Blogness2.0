using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
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
        public IHttpActionResult GetList(int postId)
        {
            try
            {
                var postContents = _postContentsSvc.GetByPostId(postId);
                return Ok(postContents);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/postcontent/{postContentId:int}")]
        public IHttpActionResult Get(int postContentId)
        {
            try
            {
                var result = _postContentsSvc.Get(postContentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new PostContent().GenerateError<PostContent>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/postcontent")]
        public IHttpActionResult Post([FromBody]PostContent postContent)
        {
            try
            {
                var result = _postContentsSvc.Add(postContent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new PostContent().GenerateError<PostContent>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/postcontent/{postContentId}")]
        public IHttpActionResult Delete(int postContentId)
        {
            try
            {
                _postContentsSvc.Delete(postContentId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }
    }
}
