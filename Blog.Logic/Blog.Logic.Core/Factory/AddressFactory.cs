using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
{
    public class AddressFactory
    {
        private AddressFactory()
        {
        }

        private static AddressFactory _instance;

        public static AddressFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AddressFactory();
                return _instance;
            }
            return _instance;
        }

        public AddressLogic CreateAddressLogic()
        {
            IAddressRepository addressRepository = new AddressRepository();
            return new AddressLogic(addressRepository);
        }
    }
}
