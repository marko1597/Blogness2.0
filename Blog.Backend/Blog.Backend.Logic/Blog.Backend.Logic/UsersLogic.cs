using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.Utils;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class UsersLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMediaRepository _mediaRepository;

        public UsersLogic(IUserRepository userRepository, IAddressRepository addressRepository, IEducationRepository educationRepository, IAlbumRepository albumRepository, IMediaRepository mediaRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _educationRepository = educationRepository;
            _albumRepository = albumRepository;
            _mediaRepository = mediaRepository;
        }

        public User GetByUserName(string userName)
        {
            User user;
            try
            {
                var tUser = _userRepository.Find(a => a.UserName == userName, null, "Address,Hobbies").FirstOrDefault();
                user = UserMapper.ToDto(tUser);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                user.Picture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.PictureId).FirstOrDefault());
                user.Background = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.BackgroundId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return user;
        }

        public User GetByCredentials(string username, string password)
        {
            User user;
            try
            {
                var tUser = _userRepository.Find(a => a.UserName == username && a.Password == password, false).FirstOrDefault();
                user = UserMapper.ToDto(tUser);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return user;
        }

        public User Get(int userId)
        {
            User user;
            try
            {
                var tUser = _userRepository.Find(a => a.UserId == userId, null, "Address,Hobbies").FirstOrDefault();
                user = UserMapper.ToDto(tUser);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                user.Picture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.PictureId).FirstOrDefault());
                user.Background = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.BackgroundId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return user;
        }

        public bool Add(User user)
        {
            try
            {
                if (user.Picture != null)
                {
                    var album = _albumRepository.Find(a => a.AlbumName == "Profile" && a.UserId == user.UserId, false).FirstOrDefault();
                    if (album != null)
                    {
                        user.Picture.AlbumId = album.AlbumId;
                        var picture = _mediaRepository.Add(MediaMapper.ToEntity(user.Picture));
                        user.Picture.MediaId = picture.MediaId;
                    }
                }

                if (user.Background != null)
                {
                    var album = _albumRepository.Find(a => a.AlbumName == "Background" && a.UserId == user.UserId, false).FirstOrDefault();
                    if (album != null)
                    {
                        user.Background.AlbumId = album.AlbumId;
                        var background = _mediaRepository.Add(MediaMapper.ToEntity(user.Background));
                        user.Background.MediaId = background.MediaId;
                    }
                }

                _userRepository.Add(UserMapper.ToEntity(user));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                _userRepository.Edit(UserMapper.ToEntity(user));
                return true;
            }
            catch (Exception)
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
