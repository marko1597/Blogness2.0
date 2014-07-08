using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IUsersLogic
    {
        User GetByUserName(string userName);
        User GetByCredentials(string username, string password);
        User Get(int userId);
        User Add(User user);
        User Update(User user);
        bool IsValidEmailAddress(string emailaddress);
    }
}
