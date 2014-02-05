using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IAddressResource
    {
        List<Address> Get(Func<Address, bool> expression);
        Address Add(Address address);
        Address Update(Address address);
        bool Delete(Address address);
    }
}
