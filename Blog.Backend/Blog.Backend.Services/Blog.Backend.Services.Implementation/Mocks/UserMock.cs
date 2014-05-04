using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class UserMock : IUser
    {
        public UserMock()
        {
            if (DataStorage.Users.Count == 0)
            {
                DataStorage.Users.Add(new User
                {
                    UserId = 1,
                    FirstName = "Jason",
                    LastName = "Magpantay",
                    UserName = "jama",
                    Password = "testtest1",
                    EmailAddress = "jason.magpantay@gmail.com",
                    BirthDate = DateTime.UtcNow.AddYears(-25)
                });
                DataStorage.Users.Add(new User
                {
                    UserId = 2,
                    FirstName = "Jason",
                    LastName = "Avel",
                    UserName = "jaav",
                    Password = "testtest1",
                    EmailAddress = "jason.avel@gmail.com",
                    BirthDate = DateTime.UtcNow.AddYears(-25)
                });
                DataStorage.Users.Add(new User
                {
                    UserId = 3,
                    FirstName = "Avel",
                    LastName = "Magpantay",
                    UserName = "avma",
                    Password = "testtest1",
                    EmailAddress = "avel.magpantay@gmail.com",
                    BirthDate = DateTime.UtcNow.AddYears(-25)
                });
            }
        }

        public User GetByCredentials(string username, string password)
        {
            var user = DataStorage.Users.FirstOrDefault(a => a.UserName == username && a.Password == password);
            return user;
        }

        public User GetByUserName(string username)
        {
            var user = DataStorage.Users.FirstOrDefault(a => a.UserName == username);
            return user;
        }

        public User Get(int userId)
        {
            var user = DataStorage.Users.FirstOrDefault(a => a.UserId == userId);
            return user;
        }

        public bool Add(User user)
        {
            var id = DataStorage.Users.Select(a => a.UserId).Max();
            user.UserId = id + 1;
            DataStorage.Users.Add(user);

            return true;
        }

        public bool Update(User user)
        {
            var tUser = DataStorage.Users.FirstOrDefault(a => a.UserId == user.UserId);
            DataStorage.Users.Remove(tUser);
            DataStorage.Users.Add(user);

            return true;
        }
    }
}
