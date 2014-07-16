using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IUsersLogic
    {
        User GetByUserName(string userName);
        User Get(int userId);
        User Add(User user);
        User Update(User user);
        bool IsValidEmailAddress(string emailaddress);
    }
}
