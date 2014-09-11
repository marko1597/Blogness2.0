using Blog.Common.Contracts;
using NUnit.Framework;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper.Tests
{
    [TestFixture]
    public class EducationTypeMapperTest
    {
        [Test]
        public void ShouldTransformEducationTypeToDto()
        {
            var param = new Db.EducationType
            {
                EducationTypeId = 1,
                EducationTypeName = "Grade School",
            };

            var result = EducationTypeMapper.ToDto(param);

            Assert.IsInstanceOf(typeof(EducationType), result);
            Assert.NotNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenPassedNullOnTransformEducationTypeToDto()
        {
            var result = EducationTypeMapper.ToDto(null);
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldTransformEducationTypeToEntity()
        {
            var param = new EducationType
            {
                EducationTypeId = 1,
                EducationTypeName = "Grade School",
            };

            var result = EducationTypeMapper.ToEntity(param);

            Assert.IsInstanceOf(typeof(Db.EducationType), result);
            Assert.NotNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenPassedNullOnTransformEducationTypeToEntity()
        {
            var result = EducationTypeMapper.ToEntity(null);
            Assert.IsNull(result);
        }
    }
}
