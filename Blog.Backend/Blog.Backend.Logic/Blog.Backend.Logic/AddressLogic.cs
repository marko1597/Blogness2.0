using System;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class AddressLogic
    {
        private readonly IAddressRepository _addressRepository;

        public AddressLogic(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Address GetByUser(int userId)
        {
            var address = new Address();
            try
            {
                var db = _addressRepository.Find(a => a.UserId == userId, true).FirstOrDefault();
                address = AddressMapper.ToDto(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return address;
        }

        public bool Add(Address address)
        {
            try
            {
                _addressRepository.Add(AddressMapper.ToEntity(address));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Address address)
        {
            try
            {
                _addressRepository.Add(AddressMapper.ToEntity(address));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int addressId)
        {
            try
            {
                var db = _addressRepository.Find(a => a.AddressId == addressId, false).FirstOrDefault();
                _addressRepository.Delete(db);
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
