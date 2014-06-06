using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Blog.Common.Utils;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using Blog.Common.Utils.Extensions;

namespace Blog.Logic.Core.Tests
{
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
        public void ShouldGetAddressByUser()
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
        public void ShouldErrorWhenGetAddressByUserFoundNoRecord()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), true))
                .Returns(new List<Address>());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.GetByUser(1);

            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No address found for user with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetAddressByUserFails()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), true))
                .Throws(new Exception());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            Assert.Throws<BlogException>(() => _addressLogic.GetByUser(1));
        }

        [Test]
        public void ShouldAddAddress()
        {
            var dbResult = new Address
            {
                AddressId = 3,
                StreetAddress = "Wiggle",
                City = "Berry",
                State = "Carrot",
                Country = "Gumbo",
                Zip = 1234,
                UserId = 5,
                User = new User
                {
                    UserId = 5,
                    UserName = "FooBar"
                }
            };
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Add(It.IsAny<Address>())).Returns(dbResult);

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.Add(new Common.Contracts.Address
            {
                AddressId = 3,
                StreetAddress = "Wiggle",
                City = "Berry",
                State = "Carrot",
                Country = "Gumbo",
                Zip = 1234
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.UserId);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddAddressFails()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Add(It.IsAny<Address>())).Throws(new Exception());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            Assert.Throws<BlogException>(() => _addressLogic.Add(new Common.Contracts.Address()));
        }

        [Test]
        public void ShouldUpdateAddress()
        {
            var dbResult = new Address
            {
                AddressId = 3,
                StreetAddress = "Wiggle",
                City = "Berry",
                State = "Carrot",
                Country = "Gumbo",
                Zip = 1234,
                UserId = 5,
                User = new User
                {
                    UserId = 5,
                    UserName = "FooBar"
                }
            };
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Edit(It.IsAny<Address>())).Returns(dbResult);

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.Update(new Common.Contracts.Address
            {
                AddressId = 3,
                StreetAddress = "Wiggle",
                City = "Berry",
                State = "Carrot",
                Country = "Gumbo",
                Zip = 1234
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.UserId);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdateAddressFails()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Edit(It.IsAny<Address>())).Throws(new Exception());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            Assert.Throws<BlogException>(() => _addressLogic.Update(new Common.Contracts.Address()));
        }

        [Test]
        public void ShouldReturnTrueOnDeleteAddress()
        {
            var dbResult = new List<Address> { new Address { AddressId = 1 } };
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
               .Returns(dbResult);

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeleteAddressFoundNoRecord()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Address, bool>>>(), false))
               .Returns(new List<Address>());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            var result = _addressLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteAddressFails()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.Delete(It.IsAny<Address>())).Throws(new Exception());

            _addressLogic = new AddressLogic(_addressRepository.Object);

            Assert.Throws<BlogException>(() => _addressLogic.Delete(1));
        }
    }
}
