using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
{
    public class UsersFactory
    {
        private UsersFactory()
        {
        }

        private static UsersFactory _instance;

        public static UsersFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UsersFactory();
                return _instance;
            }
            return _instance;
        }

        public UsersLogic CreateLogic()
        {
            IUserRepository userRepository = new UserRepository();
            IAddressRepository addressRepository = new AddressRepository();
            IEducationRepository educationRepository = new EducationRepository();
            IMediaRepository mediaRepository = new MediaRepository();

            return new UsersLogic(
                userRepository, 
                addressRepository, 
                educationRepository, 
                mediaRepository);
        }
    }
}
