﻿using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IUsersResource : IBaseResource
    {
        User GetByUserName(string username);
        User Get(int userId);
        User Add(User user);
        User Update(User user);
    }
}
