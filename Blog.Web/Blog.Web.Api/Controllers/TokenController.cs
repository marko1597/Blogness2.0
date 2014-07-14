using System.Threading.Tasks;
using System.Web.Http;
using Blog.Common.Identity;
using Blog.Common.Identity.Interfaces;
using Microsoft.AspNet.Identity;

namespace Blog.Web.Api.Controllers
{
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        private readonly IBlogDbRepository _blogDbRepository;

        public TokenController(IBlogDbRepository blogDbRepository)
        {
            _blogDbRepository = blogDbRepository;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]BlogRegisterModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _blogDbRepository.RegisterUser(userModel);
            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }
        
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}
