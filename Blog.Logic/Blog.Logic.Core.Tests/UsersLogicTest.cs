using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class UsersLogicTest
    {
        #region Objects

        private List<Education> _educations;
        private List<Address> _addresses;
        private List<User> _users;
        private List<Album> _albums;
        private List<Media> _mediae;
        private List<Hobby> _hobbies; 

        #endregion

        private UsersLogic _usersLogic;
        private Mock<IUserRepository> _userRepository;
        private Mock<IAddressRepository> _addressRepository;
        private Mock<IEducationRepository> _educationRepository;
        private Mock<IMediaRepository> _mediaRepository;

        [SetUp]
        public void TestInit()
        {
            #region Educations

            _educations = new List<Education>();

            for (var i = 1; i <= 3; i++)
            {
                _educations.Add(new Education
                {
                    EducationTypeId = 1,
                    UserId = i,
                    SchoolName = "Grade School",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.Now.AddYears(-20),
                    YearGraduated = DateTime.Now.AddYears(-14),
                    Course = string.Empty,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });

                _educations.Add(new Education
                {
                    EducationTypeId = 2,
                    UserId = i,
                    SchoolName = "High School",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.Now.AddYears(-14),
                    YearGraduated = DateTime.Now.AddYears(-8),
                    Course = string.Empty,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });

                _educations.Add(new Education
                {
                    EducationTypeId = 3,
                    UserId = i,
                    SchoolName = "College Education",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.Now.AddYears(-8),
                    YearGraduated = DateTime.Now.AddYears(-4),
                    Course = "BS Computer Science",
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });
            }

            #endregion

            #region Addresses

            _addresses = new List<Address>();

            for (var i = 1; i <= 3; i++)
            {
                _addresses.Add(new Address
                {
                    UserId = i,
                    StreetAddress = "Street Address",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    Zip = 1234
                });
            }

            #endregion

            #region Hobbies

            _hobbies = new List<Hobby>();

            for (var i = 1; i <= 3; i++)
            {
                _hobbies.Add(new Hobby
                {
                    HobbyName = "Fooing",
                    UserId = i,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });
            }

            #endregion

            #region Albums

            _albums = new List<Album>();

            for (var i = 1; i <= 3; i++)
            {
                _albums.Add(new Album
                {
                    AlbumName = "Foo",
                    UserId = i,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now,
                    IsUserDefault = true
                });
            }

            #endregion

            #region Mediae

            _mediae = new List<Media>();

            for (var i = 1; i <= 3; i++)
            {
                _mediae.Add(new Media
                {
                    CustomName = Guid.NewGuid().ToString(),
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now,
                    AlbumId = i,
                    FileName = "foo.jpg",
                    MediaUrl = "http://mock.object/foo",
                    MediaType = "image/jpeg",
                    MediaPath = @"C:/bar/foo.jpg",
                    ThumbnailUrl = "http://mock.object/foo/thumb",
                    ThumbnailPath = @"C:/bar/tn/tn_foo.jpg",
                });
            }

            #endregion

            #region Users

            _users = new List<User>
                        {
                            new User
                            {
                                UserId = 1,
                                FirstName = "Jason",
                                LastName = "Magpantay",
                                UserName = "jama",
                                IdentityId = "testtest1",
                                EmailAddress = "jason.magpantay@gmail.com",
                                BirthDate = DateTime.Now.AddYears(-25),
                                PictureId = 1,
                                BackgroundId = 1,
                                Hobbies = _hobbies.Where(a => a.UserId == 1).ToList()
                            },
                            new User
                            {
                                UserId = 2,
                                FirstName = "Jason",
                                LastName = "Avel",
                                UserName = "jaav",
                                IdentityId = "testtest1",
                                EmailAddress = "jason.avel@gmail.com",
                                BirthDate = DateTime.Now.AddYears(-25),
                                PictureId = 2,
                                BackgroundId = 2,
                                Hobbies = _hobbies.Where(a => a.UserId == 2).ToList()
                            },
                            new User
                            {
                                UserId = 3,
                                FirstName = "Avel",
                                LastName = "Magpantay",
                                UserName = "avma",
                                IdentityId = "testtest1",
                                EmailAddress = "avel.magpantay@gmail.com",
                                BirthDate = DateTime.Now.AddYears(-25),
                                PictureId = 3,
                                BackgroundId = 3,
                                Hobbies = _hobbies.Where(a => a.UserId == 3).ToList()
                            }
                        };

            #endregion
        }
        
        [Test]
        public void ShouldGetUserById()
        {
            const int userId = 1;
            var educations = _educations.Where(a => a.UserId == userId).ToList();

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
                .Returns(_addresses);

            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), true))
                .Returns(educations);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), null, string.Empty))
                .Returns(_mediae);

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Get(userId);

            Assert.NotNull(user);
            Assert.NotNull(user.Address);
            Assert.NotNull(user.Education);
            Assert.NotNull(user.Hobbies);
            Assert.NotNull(user.Picture);
            Assert.NotNull(user.Background);
            Assert.AreEqual(user.Id, userId);
        }

        [Test]
        public void ShouldGetEmptyAddressWhenGetUserHasNoId()
        {
            const int userId = 1;
            var educations = _educations.Where(a => a.UserId == userId).ToList();
            var dbUser = _users.Where(a => a.UserId == 1).ToList();
            dbUser[0].UserId = 0;

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(dbUser);

            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
                .Returns(_addresses);

            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), true))
                .Returns(educations);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), null, string.Empty))
                .Returns(_mediae);

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Get(userId);

            Assert.NotNull(user);
            Assert.NotNull(user.Address);
            Assert.AreEqual(0, user.Address.AddressId);
        }

        [Test]
        public void ShouldGetEmptyEducationListWhenGetUserHasNoId()
        {
            const int userId = 1;
            var educations = _educations.Where(a => a.UserId == userId).ToList();
            var dbUser = _users.Where(a => a.UserId == 1).ToList();
            dbUser[0].UserId = 0;

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(dbUser);

            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
                .Returns(_addresses);

            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), true))
                .Returns(educations);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), null, string.Empty))
                .Returns(_mediae);

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Get(userId);

            Assert.NotNull(user);
            Assert.NotNull(user.Address);
            Assert.NotNull(user.Education);
            Assert.AreEqual(0, user.Education.Count);
        }

        [Test]
        public void ShouldGetUserByIdentityId()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.GetByIdentity("lorem-ipsum-dolor-sit-amet");

            Assert.NotNull(user);
            Assert.IsNull(user.Error);
            Assert.IsInstanceOf(typeof(Common.Contracts.User), user);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetUserByIdentityIdFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _usersLogic.GetByIdentity("lorem-ipsum-dolor-sit-amet"));
        }

        [Test]
        public void ShouldErrorWhenGetUserByIdentityIdHasNoResult()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(new List<User>());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.GetByIdentity("lorem-ipsum-dolor-sit-amet");

            Assert.IsNotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.RecordNotFound);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetUserByIdFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _usersLogic.Get(1));
        }

        [Test]
        public void ShouldErrorWhenGetUserByIdHasNoResult()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(new List<User>());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Get(5);

            Assert.IsNotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.RecordNotFound);
        }

        [Test]
        public void ShouldGetUsersWithNoIdentityIdSuccessfully()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var users = _usersLogic.GetUsersWithNoIdentityId();

            Assert.NotNull(users);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.User>), users);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetUsersWithNoIdentityIdFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var result = Assert.Throws<BlogException>(() => _usersLogic.GetUsersWithNoIdentityId());

            Assert.IsInstanceOf(typeof(BlogException), result);
        }

        [Test]
        public void ShouldGetUsersListSuccessfully()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.GetUsers(It.IsAny<int>(), It.IsAny<int>())).Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var users = _usersLogic.GetUsers(5, 5);

            Assert.NotNull(users);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.User>), users);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetUsersFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.GetUsers(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var result = Assert.Throws<BlogException>(() => _usersLogic.GetUsers(5, 5));

            Assert.IsInstanceOf(typeof(BlogException), result);
        }

        [Test]
        public void ShouldGetUsersByCommunitySuccessfully()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.GetUsersByCommunity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var users = _usersLogic.GetUsersByCommunity(1, 5, 10);

            Assert.NotNull(users);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.User>), users);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetUsersByCommunityFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.GetUsersByCommunity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var users = Assert.Throws<BlogException>(() => _usersLogic.GetUsersByCommunity(1, 5, 10));

            Assert.IsInstanceOf(typeof(BlogException), users);
        }

        [Test]
        public void ShouldGetUserByUsername()
        {
            const string username = "jama";
            var educations = _educations.Where(a => a.UserId == 1).ToList();

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
                .Returns(_addresses);

            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), true))
                .Returns(educations);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), null, string.Empty))
                .Returns(_mediae);

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.GetByUserName(username);

            Assert.NotNull(user);
            Assert.NotNull(user.Address);
            Assert.NotNull(user.Education);
            Assert.NotNull(user.Hobbies);
            Assert.NotNull(user.Picture);
            Assert.NotNull(user.Background);
            Assert.AreEqual(user.UserName, username);
        }
        
        [Test]
        public void ShouldThrowExceptionWhenGetUserByUsernameFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _usersLogic.GetByUserName("foo"));
        }

        [Test]
        public void ShouldErrorWhenGetUserByUsernameHasNoResult()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(new List<User>());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.GetByUserName("foo");

            Assert.IsNotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.RecordNotFound);
        }

        [Test]
        public void ShouldAddUser()
        {
            var request = new Common.Contracts.User
                           {
                               UserName = "foo",
                               IdentityId = "fooBar12345!",
                               FirstName = "foo",
                               LastName = "bar",
                               EmailAddress = "test@mgail.com",
                               BirthDate = DateTime.Now
                           };
            var result = new User
                           {
                               UserName = "foo",
                               IdentityId = "fooBar12345!",
                               FirstName = "foo",
                               LastName = "bar",
                               EmailAddress = "test@gmail.com",
                               BirthDate = DateTime.Now
                           };

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Add(It.IsAny<User>()))
                .Returns(result);
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(new List<User>());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user);
            Assert.IsNull(user.Error);
        }

        [Test]
        public void ShouldErrorWhenAddUserHasNoUsername()
        {
            var request = new Common.Contracts.User
            {
                IdentityId = "fooBar12345!",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mail.com",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Username cannot be empty");
        }

        [Test]
        public void ShouldErrorWhenAddUserHasNoFirstName()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "bar",
                LastName = "bar",
                EmailAddress = "test@mail.com",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "First name cannot be empty");
        }

        [Test]
        public void ShouldErrorWhenAddUserHasNoLastName()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "bar",
                FirstName = "foo",
                EmailAddress = "test@mail.com",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Last name cannot be empty");
        }

        [Test]
        public void ShouldErrorWhenAddUserHasNoEmail()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "bar",
                FirstName = "foo",
                LastName = "bar",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Email address cannot be empty");
        }

        [Test]
        public void ShouldErrorWhenAddUserHasNoIdentityId()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                EmailAddress = "foo@mail.com",
                FirstName = "foo",
                LastName = "bar",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Identity Id cannot be empty");
        }

        [Test]
        public void ShouldErrorWhenAddUserHasNoBirthdate()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "bar",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mail.com"
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Birth date cannot be empty");
        }

        [Test]
        public void ShouldErrorWhenAddUserHasInvalidEmail()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "bar",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Invalid email address");
        }

        [Test]
        public void ShouldErrorWhenAddUserWhenUserExists()
        {
            var request = new Common.Contracts.User
            {
                UserName = "jama",
                IdentityId = "bar",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@gmail.com",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(_users);

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Add(request);

            Assert.NotNull(user.Error);
            Assert.AreEqual(user.Error.Id, (int)Constants.Error.ValidationError);
            Assert.AreEqual(user.Error.Message, "Username already exists");
        }

        [Test]
        public void ShouldThrowExceptionWhenAddUserFails()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "fooBar12345!",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mgail.com",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Add(It.IsAny<User>()))
                .Throws(new Exception());
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(new List<User>());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _usersLogic.Add(request));
        }

        [Test]
        public void ShouldUpdateUser()
        {
            var request = new Common.Contracts.User
            {
                UserName = "foo",
                IdentityId = "fooBar12345!",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mgail.com",
                BirthDate = DateTime.Now
            };
            var result = new User
            {
                UserName = "foo",
                IdentityId = "fooBar12345!",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@gmail.com",
                BirthDate = DateTime.Now
            };

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Edit(It.IsAny<User>()))
                .Returns(result);

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            var user = _usersLogic.Update(request);

            Assert.NotNull(user);
            Assert.IsNull(user.Error);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdateUserFails()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Edit(It.IsAny<User>()))
                .Throws(new Exception());

            _addressRepository = new Mock<IAddressRepository>();
            _educationRepository = new Mock<IEducationRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _usersLogic = new UsersLogic(_userRepository.Object, _addressRepository.Object,
                _educationRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _usersLogic.Update(new Common.Contracts.User()));
        }
    }
}
