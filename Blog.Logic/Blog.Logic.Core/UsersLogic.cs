using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core
{
    public class UsersLogic
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

        public User GetByCredentials(string username, string password)
        {
            try
            {
                var tUser = _userRepository.Find(a => a.UserName == username && a.Password == password, null, string.Empty).FirstOrDefault();

                if (tUser != null)
                {
                    var user = UserMapper.ToDto(tUser);
                    return user;
                }
                return new User
                {
                    Error = new Error
                    {
                        Id = (int)Constants.Error.InvalidCredentials,
                        Message = "Invalid credentials"
                    }
                };
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
                    dbUser = _userRepository.Add(dbUser);
                    return UserMapper.ToDto(dbUser);
                }

                return new User
                {
                    Error = hasPassedValidations
                };
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
                _userRepository.Find(a => a.UserId == userId, null, "Address,Hobbies").FirstOrDefault() : 
                _userRepository.Find(a => a.UserName == username, null, "Address,Hobbies").FirstOrDefault();

            if (tUser != null)
            {
                var user = UserMapper.ToDto(tUser);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                user.Picture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.PictureId).FirstOrDefault());
                user.Background = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.BackgroundId).FirstOrDefault());
                return user;
            }

            return new User
            {
                Error = new Error
                {
                    Id = (int)Constants.Error.RecordNotFound,
                    Message = "Record not found"
                }
            };
        }

        private Error ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return new Error { Id = (int) Constants.Error.ValidationError,  Message = "Username cannot be empty" };
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Password cannot be empty" };
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

            return (int) PasswordManager.CheckStrength(user.Password) > 2
                ? null
                : new Error {Id = (int) Constants.Error.ValidationError, Message = "Password is not too complex"};
        }

        private static Db.User PrepareUserForAdding(User user)
        {
            var dbUser = UserMapper.ToEntity(user);
            dbUser.Albums = new Collection<Db.Album>
            {
                new Db.Album
                {
                    AlbumName = "Miscellaneous",
                    IsUserDefault = true,
                    CreatedBy = dbUser.UserId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = dbUser.UserId,
                    ModifiedDate = DateTime.Now
                },
                new Db.Album
                {
                    AlbumName = "Profile",
                    IsUserDefault = false,
                    CreatedBy = dbUser.UserId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = dbUser.UserId,
                    ModifiedDate = DateTime.Now
                },
                new Db.Album
                {
                    AlbumName = "Background",
                    IsUserDefault = false,
                    CreatedBy = dbUser.UserId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = dbUser.UserId,
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
            if (user.UserId <= 0) return address;

            address = AddressMapper.ToDto(_addressRepository.Find(a => a.UserId == user.UserId, false).FirstOrDefault());
            return address;
        }

        private List<Education> GetEducations(User user)
        {
            var educations = new List<Education>();
            if (user.UserId <= 0) return educations;

            var db = _educationRepository.Find(a => a.UserId == user.UserId, true).ToList();
            db.ForEach(a => educations.Add(EducationMapper.ToDto(a)));

            return educations;
        }
    }
}
