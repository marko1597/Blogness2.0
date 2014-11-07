using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core
{
    public class UsersLogic : IUsersLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IMediaRepository _mediaRepository;

        public UsersLogic(
            IUserRepository userRepository, 
            IAddressRepository addressRepository, 
            IEducationRepository educationRepository, 
            IMediaRepository mediaRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _educationRepository = educationRepository;
            _mediaRepository = mediaRepository;
        }

        public List<User> GetUsers(int threshold = 10, int skip = 10)
        {
            try
            {
                var db = _userRepository.GetUsers(threshold, skip).ToList();
                return db.Select(UserMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<User> GetUsersWithNoIdentityId()
        {
            try
            {
                var db = _userRepository.Find(a => string.IsNullOrEmpty(a.IdentityId), null, null).ToList();
                return db.Select(UserMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public User GetByUserName(string userName)
        {
            try
            {
                return GetUser(null, userName);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public User GetByIdentity(string identityId)
        {
            try
            {
                var dbUser = _userRepository.Find(a => a.IdentityId == identityId, null, null).FirstOrDefault();
                if (dbUser != null) return UserMapper.ToDto(dbUser);

                return new User().GenerateError<User>((int)Constants.Error.RecordNotFound, "User not found"); 
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public User Get(int userId)
        {
            try
            {
                return GetUser(userId, string.Empty);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public User Add(User user)
        {
            try
            {
                var hasPassedValidations = ValidateUser(user);

                if (hasPassedValidations == null)
                {
                    var dbUser = PrepareUserForAdding(user);
                    user.IsDeleted = false;
                    dbUser = _userRepository.Add(dbUser);
                    return UserMapper.ToDto(dbUser);
                }

                return new User().GenerateError<User>(hasPassedValidations.Id, hasPassedValidations.Message);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public User Update(User user)
        {
            try
            {
                var tUser = _userRepository.Edit(UserMapper.ToEntity(user));
                return UserMapper.ToDto(tUser);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private User GetUser(int? userId, string username)
        {
            var tUser = string.IsNullOrEmpty(username) ? 
                _userRepository.Find(a => a.UserId == userId, null, "Address,Hobbies,Picture,Background").FirstOrDefault() :
                _userRepository.Find(a => a.UserName == username, null, "Address,Hobbies,Picture,Background").FirstOrDefault();

            if (tUser != null)
            {
                var user = UserMapper.ToDto(tUser);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                user.Picture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.PictureId).FirstOrDefault());
                user.Background = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.BackgroundId).FirstOrDefault());
                return user;
            }

            return new User().GenerateError<User>((int) Constants.Error.RecordNotFound, "User not found");
        }

        private Error ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return new Error { Id = (int) Constants.Error.ValidationError,  Message = "Username cannot be empty" };
            }

            if (string.IsNullOrEmpty(user.IdentityId))
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Identity Id cannot be empty" };
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "First name cannot be empty" };
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Last name cannot be empty" };
            }

            if (string.IsNullOrEmpty(user.EmailAddress))
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Email address cannot be empty" };
            }

            if (user.BirthDate == DateTime.MinValue )
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Birth date cannot be empty" };
            }

            if (!IsValidEmailAddress(user.EmailAddress))
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Invalid email address" };
            }

            var tUser = _userRepository.Find(a => a.UserName == user.UserName, false).ToList();
            if (tUser.Count > 0)
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Username already exists" };
            }

            return null;
        }

        private static Db.User PrepareUserForAdding(User user)
        {
            var dbUser = UserMapper.ToEntity(user);
            dbUser.Address = new Db.Address
            {
                StreetAddress = string.Empty,
                State = string.Empty,
                City = string.Empty,
                Country = string.Empty,
                Zip = null
            };
            dbUser.Albums = new Collection<Db.Album>
            {
                new Db.Album
                {
                    AlbumName = "Miscellaneous",
                    IsUserDefault = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Db.Album
                {
                    AlbumName = "Profile",
                    IsUserDefault = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Db.Album
                {
                    AlbumName = "Background",
                    IsUserDefault = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };

            return dbUser;
        }

        public bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private Address GetAddress(User user)
        {
            var address = new Address();
            if (user.Id <= 0) return address;

            address = AddressMapper.ToDto(_addressRepository.Find(a => a.UserId == user.Id, false).FirstOrDefault());
            return address;
        }

        private List<Education> GetEducations(User user)
        {
            var educations = new List<Education>();
            if (user.Id <= 0) return educations;

            var db = _educationRepository.Find(a => a.UserId == user.Id, true).ToList();
            db.ForEach(a => educations.Add(EducationMapper.ToDto(a)));

            return educations;
        }
    }
}
