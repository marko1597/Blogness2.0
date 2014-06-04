using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using Blog.Common.Utils;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using Blog.Common.Utils.Extensions;

namespace Blog.Logic.Core.Tests
{
    /// <summary>
    /// Summary description for AddressLogicTest
    /// </summary>
    [TestFixture]
    public class AddressLogicTest
    {
        private Mock<IAddressRepository> _addressRepository;

        private AddressLogic _addressLogic;

        private List<Address> _addresses;

        [SetUp]
        public void TestInit()
        {
            #region Addresses

            _addresses = new List<Address>
                     {
                         new Address
                         {
                             AddressId = 1,
                             StreetAddress = "Foo",
                             City = "Bar",
                             State = "Baz",
                             Country = "Fish",
                             UserId = 1,
                             Zip = 1234,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    }
                         },
                         new Address
                         {
                             AddressId = 2,
                             StreetAddress = "Lorem",
                             City = "Ipsum",
                             State = "Dolor",
                             Country = "Sit",
                             UserId = 2,
                             Zip = 1234,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Amet"
                                    }
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetByUser()
        {
            var expected = _addresses.Where(a => a.UserId == 1).ToList();
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), true))
                .Returns(expected);

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.GetByUser(1);

            Assert.AreEqual(1, result.AddressId);
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public void ShouldErrorWhenGetByUserFoundNoRecord()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), true))
                .Returns(new List<Address>());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.GetByUser(1);

            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No address found for userId 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionGetByUserFails()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), true))
                .Throws(new Exception());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            Assert.Throws<BlogException>(() => _addressLogic.GetByUser(1));
        }
    }
}
