using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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

        public UsersLogic CreateUsers()
        {
            IUserRepository userRepository = new UserRepository();
            IAddressRepository addressRepository = new AddressRepository();
            IEducationRepository educationRepository = new EducationRepository();
            IMediaRepository mediaRepository = new MediaRepository();
            IAlbumRepository albumRepository = new AlbumRepository();
            return new UsersLogic(userRepository, addressRepository, educationRepository, mediaRepository, albumRepository);
        }
    }
}
