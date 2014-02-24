using System;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class UsersLogic
    {
        private readonly IUserRepository _userRepository;

        public UsersLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetByUserName(int? userId, string userName)
        {
            var user = new User();
            try
            {
                user = string.IsNullOrEmpty(userName) ? 
                    UserMapper.ToDto(_userRepository.Find(a => a.UserId == userId, true).FirstOrDefault()) : 
                    UserMapper.ToDto(_userRepository.Find(a => a.UserName == userName, true).FirstOrDefault());
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
                user = UserMapper.ToDto(_userRepository.Find(a => a.UserName == username && a.Password == password, true).FirstOrDefault());
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
                user = UserMapper.ToDto(_userRepository.Find(a => a.UserId == userId, true).FirstOrDefault());
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
    }
}
