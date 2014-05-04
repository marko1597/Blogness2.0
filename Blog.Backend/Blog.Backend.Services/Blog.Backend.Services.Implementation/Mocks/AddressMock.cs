using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class AddressMock : IAddress
    {
        public AddressMock()
        {
            if (DataStorage.Addresses.Count == 0)
            {
                var addressId = 1;

                DataStorage.Users.ForEach(a =>
                                        {
                                            DataStorage.Addresses.Add(new Address
                                            {
                                                AddressId = addressId,
                                                UserId = a.UserId,
                                                StreetAddress = "Street Address",
                                                City = "City",
                                                State = "State",
                                                Country = "Country",
                                                Zip = 1234
                                            });
                                            addressId++;
                                        });
            }
        }

        public Address GetByUser(int userId)
        {
            var address = DataStorage.Addresses.FirstOrDefault(a => a.UserId == userId);
            return address;
        }

        public bool Add(Address address)
        {
            var id = DataStorage.Addresses.Select(a => a.AddressId).Max();
            address.AddressId = id + 1;
            DataStorage.Addresses.Add(address);

            return true;
        }

        public bool Update(Address address)
        {
            var tAddress = DataStorage.Addresses.FirstOrDefault(a => a.AddressId == address.AddressId);
            DataStorage.Addresses.Remove(tAddress);
            DataStorage.Addresses.Add(address);

            return true;
        }

        public bool Delete(int addressId)
        {
            var tAddress = DataStorage.Addresses.FirstOrDefault(a => a.AddressId == addressId);
            DataStorage.Addresses.Remove(tAddress);

            return true;
        }
    }
}
