﻿using Blog.Common.Contracts;

namespace Blog.Services.Implementation
{
    public interface IAddress
    {
        Address GetByUser(int userId);
        bool Add(Address address);
        bool Update(Address address);
        bool Delete(int addressId);
    }
}
