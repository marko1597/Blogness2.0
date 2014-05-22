using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class UserMock : IUser
    {
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
