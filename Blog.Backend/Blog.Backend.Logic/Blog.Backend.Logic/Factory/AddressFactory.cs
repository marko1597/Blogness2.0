using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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
