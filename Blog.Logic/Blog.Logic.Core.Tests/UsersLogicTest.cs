using System;
using System.Linq;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    public class UsersLogicTest
    {
        private UsersLogic _usersLogic;
        private Mock<IUserRepository> _userRepository;
        private Mock<IAddressRepository> _addressRepository;
        private Mock<IEducationRepository> _educationRepository;
        private Mock<IAlbumRepository> _albumRepository;
        private Mock<IMediaRepository> _mediaRepository;

        [SetUp]
        public void TestInit()
        {
            
        }
        
        [Test]
        public void ShouldGetUserById()
        {
            const int userId = 1;
            var users = new ObjectMocks().GenerateUsers();
            var addresses = new ObjectMocks().GenerateAddresses();
            var mediae = new ObjectMocks().GenerateMedia();
            var educations = new ObjectMocks().GenerateEducations();

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(users);
            //_userRepository.Setup(moq => moq.Find(a => a.UserId == userId, null, "Address,Hobbies"))
            //    .Returns(users);

            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
                .Returns(addresses);

            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), false))
                .Returns(educations);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
                .Returns(mediae);

            _albumRepository = new Mock<IAlbumRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _albumRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Get(userId);
            Assert.NotNull(user);
        }
    }
}
