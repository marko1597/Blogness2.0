using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Identity.User;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Web.Api.Models;
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
        public IHttpActionResult Get(int userId)
        {
            try
            {
                var tUser = _user.Get(userId);
                if (tUser.Error != null) throw new Exception(tUser.Error.Message);

                tUser = HideUserProperties(tUser);
                var user = GetViewModel(tUser);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 20, ServerTimeSpan = 20)]
        [Route("api/users/{name}")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                var tUser = _user.GetByUserName(name);
                if (tUser.Error != null) throw new Exception(tUser.Error.Message);

                tUser = HideUserProperties(tUser);
                var user = GetViewModel(tUser);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/users")]
        public IHttpActionResult Post([FromBody] User user)
        {
            try
            {
                var tUser = _user.Add(user);
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
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = "Oops! That's not supposed to happen. Can you try again?"
                    }
                };

                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
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
                if (tUser.Error != null) throw new Exception(tUser.Error.Message);

                var updatedUser = _user.GetByUserName(user.UserName);
                if (updatedUser.Error != null) throw new Exception(updatedUser.Error.Message);

                updatedUser = HideUserProperties(updatedUser);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new User
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = "Oops! That's not supposed to happen. Can you try again?"
                    }
                };

                return Ok(errorResult);
            }
        }

        private static UserProfileViewModel GetViewModel(User user)
        {
            var userViewModel = new UserProfileViewModel
                                {
                                    Id = user.Id,
                                    UserName = user.UserName,
                                    IdentityId = user.IdentityId,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    EmailAddress = user.EmailAddress,
                                    BirthDate = user.BirthDate,
                                    Address = user.Address,
                                    Picture = user.Picture,
                                    PictureId = user.PictureId,
                                    Background = user.Background,
                                    BackgroundId = user.BackgroundId,
                                    Hobbies = user.Hobbies
                                };

            var educationGroups = new List<EducationGroup>();

            var gradeSchool = user.Education.Where(a => a.EducationType.EducationTypeId == 1).ToList();
            educationGroups.Add(new EducationGroup
            {
                EducationType = 1,
                Title = "Grade School",
                Content = gradeSchool
            });

            var highSchool = user.Education.Where(a => a.EducationType.EducationTypeId == 2).ToList();
            educationGroups.Add(new EducationGroup
            {
                EducationType = 2,
                Title = "High School",
                Content = highSchool
            });

            var college = user.Education.Where(a => a.EducationType.EducationTypeId == 3).ToList();
            educationGroups.Add(new EducationGroup
            {
                EducationType = 3,
                Title = "College",
                Content = college
            });

            var graduateSchool = user.Education.Where(a => a.EducationType.EducationTypeId == 4).ToList();
            educationGroups.Add(new EducationGroup
            {
                EducationType = 4,
                Title = "Graduate School",
                Content = graduateSchool
            });

            userViewModel.EducationGroups = educationGroups;

            return userViewModel;
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
