using System;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class AddressLogic : IAddressLogic
    {
        private readonly IAddressRepository _addressRepository;

        public AddressLogic(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Address GetByUser(int userId)
        {
            try
            {
                var db = _addressRepository.Find(a => a.UserId == userId, true).FirstOrDefault();

                if (db != null)
                {
                    return AddressMapper.ToDto(db);
                }

                return new Address().GenerateError<Address>(
                    (int) Constants.Error.RecordNotFound,
                    string.Format("No address found for user with Id {0}", userId));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Address Add(Address address)
        {
            try
            {
                return AddressMapper.ToDto(_addressRepository.Add(AddressMapper.ToEntity(address)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Address Update(Address address)
        {
            try
            {
                return AddressMapper.ToDto(_addressRepository.Edit(AddressMapper.ToEntity(address)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int addressId)
        {
            try
            {
                var db = _addressRepository.Find(a => a.AddressId == addressId, false).FirstOrDefault();
                if (db == null) return false;

                _addressRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
