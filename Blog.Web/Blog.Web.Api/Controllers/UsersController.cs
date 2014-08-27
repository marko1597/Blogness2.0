using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Identity;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    public class UsersController : ApiController
    {
        private BlogUserManager _userManager;
        private readonly IUsersResource _user;
        private readonly IErrorSignaler _errorSignaler;

        public BlogUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<BlogUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UsersController(BlogUserManager userManager, IUsersResource user, IErrorSignaler errorSignaler)
        {
            UserManager = userManager;
            _user = user;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("api/users/{userId:int}")]
        public User Get(int userId)
        {
            var user = new User();

            try
            {
                user = _user.Get(userId) ?? new User();
                user = HideUserProperties(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return user;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("api/users/{name}")]
        public User Get(string name)
        {
            var user = new User();

            try
            {
                user = _user.GetByUserName(name) ?? new User();
                user = HideUserProperties(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return user;
        }

        [HttpPost]
        [Route("api/users")]
        public User Post([FromBody] User user)
        {
            try
            {
                var tUser = _user.Add(user);
                tUser = HideUserProperties(tUser);

                return tUser;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);

                return new User
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = "Oops! That's not supposed to happen. Can you try again?"
                    }
                };
            }
        }

        [HttpPut]
        [Route("api/users")]
        [Authorize]
        public async Task<IHttpActionResult> Put([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var identityUser = await UserManager.FindByNameAsync(user.UserName);
                user.IdentityId = identityUser.Id;

                var tUser = _user.Update(user);
                tUser = HideUserProperties(tUser);

                return Ok(tUser);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new User
                {
                    Error = new Error
                    {
                        Id = (int) Common.Utils.Constants.Error.InternalError,
                        Message = "Oops! That's not supposed to happen. Can you try again?"
                    }
                };

                return Ok(errorResult);
            }
        }

        private static User HideUserProperties(User user)
        {
            if (user.Picture != null)
            {
                user.Picture.FileName = null;
                user.Picture.MediaPath = null;
                user.Picture.ThumbnailPath = null;
            }
            if (user.Background != null)
            {
                user.Background.FileName = null;
                user.Background.MediaPath = null;
                user.Background.ThumbnailPath = null;
            }

            return user;
        }
    }
}
