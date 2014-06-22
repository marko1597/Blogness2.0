using Blog.Common.Contracts;
using NUnit.Framework;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper.Tests
{
    [TestFixture]
    public class AddressMapperTest
    {
        [Test]
        public void ShouldTransformAddressToDto()
        {
            var param = new Db.Address
            {
                AddressId = 1,
                City = "Ipsum City",
                Country = "Dolor Republic",
                State = "Lorem State",
                StreetAddress = "Foo Street",
                Zip = 1,
                UserId = 1,
                User = new Db.User { UserId = 1 }
            };

            var result = AddressMapper.ToDto(param);

            Assert.IsInstanceOf(typeof(Address), result);
            Assert.NotNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenPassedNullOnTransformAddressToDto()
        {
            var result = AddressMapper.ToDto(null);
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldTransformAddressToEntity()
        {
            var param = new Address
            {
                AddressId = 1,
                City = "Ipsum City",
                Country = "Dolor Republic",
                State = "Lorem State",
                StreetAddress = "Foo Street",
                Zip = 1,
                UserId = 1
            };

            var result = AddressMapper.ToEntity(param);

            Assert.IsInstanceOf(typeof(Address), result);
            Assert.IsNull(result.User);
            Assert.NotNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenPassedNullOnTransformAddressToEntity()
        {
            var result = AddressMapper.ToEntity(null);
            Assert.IsNull(result);
        }
    }
}
