﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class UsersLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IAlbumRepository _albumRepository;

        public UsersLogic(IUserRepository userRepository, IAddressRepository addressRepository, IEducationRepository educationRepository, IMediaRepository mediaRepository, IAlbumRepository albumRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _educationRepository = educationRepository;
            _mediaRepository = mediaRepository;
            _albumRepository = albumRepository;
        }

        public User GetByUserName(string userName)
        {
            var user = new User();
            try
            {
                var tUser = _userRepository.Find(a => a.UserName == userName, null, "Address,Hobbies").FirstOrDefault();
                user = UserMapper.ToDto(tUser, true);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                //user.UserPicture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.UserPictureId).FirstOrDefault(), false);
                //user.UserBackground = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.UserBackgroundId).FirstOrDefault(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public User GetByCredentials(string username, string password)
        {
            var user = new User();
            try
            {
                var tUser = _userRepository.Find(a => a.UserName == username && a.Password == password, null, "Address,Hobbies").FirstOrDefault();
                user = UserMapper.ToDto(tUser, false);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                //user.UserPicture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.UserPictureId).FirstOrDefault(), false);
                //user.UserBackground = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.UserBackgroundId).FirstOrDefault(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public User Get(int userId)
        {
            var user = new User();
            try
            {
                var tUser = _userRepository.Find(a => a.UserId == userId, null, "Address,Hobbies").FirstOrDefault();
                user = UserMapper.ToDto(tUser, true);
                user.Address = GetAddress(user);
                user.Education = GetEducations(user);
                //user.UserPicture = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.UserPictureId).FirstOrDefault(), false);
                //user.UserBackground = MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == tUser.UserBackgroundId).FirstOrDefault(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public bool Add(User user)
        {
            try
            {
                var tUser = _userRepository.Find(a => a.UserId == user.UserId).FirstOrDefault();

                if (user.UserPicture != null)
                {
                    var album = _albumRepository.Find(a => a.AlbumName == "Profile" && a.UserId == user.UserId).FirstOrDefault();
                    if (album != null)
                    {
                        user.UserPicture.AlbumId = album.AlbumId;
                        var media = MediaFactory.GetInstance().CreateMedia().Add(user.UserPicture);
                        if (tUser != null) tUser.UserPictureId = media.MediaId;
                    }
                }

                if (user.UserBackground != null)
                {
                    var album = _albumRepository.Find(a => a.AlbumName == "Background" && a.UserId == user.UserId).FirstOrDefault();
                    if (album != null)
                    {
                        user.UserBackground.AlbumId = album.AlbumId;
                        var media = MediaFactory.GetInstance().CreateMedia().Add(user.UserBackground);
                        if (tUser != null) tUser.UserBackgroundId = media.MediaId;
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
