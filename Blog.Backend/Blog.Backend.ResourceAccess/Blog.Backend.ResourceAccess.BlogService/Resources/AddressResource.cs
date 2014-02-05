using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class AddressResource : IAddressResource
    {
        public List<Address> Get(Func<Address, bool> expression)
        {
            return new DbGet().Address(expression);
        }

        public Address Add(Address address)
        {
            return new DbAdd().Address(address);
        }

        public Address Update(Address address)
        {
            return new DbUpdate().Address(address);
        }

        public bool Delete(Address address)
        {
            return new DbDelete().Address(address);
        }
    }
}
