using System;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
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
            try
            {
                var db = _addressRepository.Find(a => a.UserId == userId, true).FirstOrDefault();

                if (db != null)
                {
                    return AddressMapper.ToDto(db);
                }

                return new Address
                {
                    Error = new Error
                    {
                        Id = (int) Constants.Error.RecordNotFound,
                        Message = string.Format("No address found for userId {0}", userId)
                    }
                };
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
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
