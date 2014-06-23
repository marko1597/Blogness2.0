using System;
using Blog.Common.Contracts;
using NUnit.Framework;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper.Tests
{
    [TestFixture]
    class EducationMapperTest
    {
        [Test]
        public void ShouldTransformEducationToDto()
        {
            var param = new Db.Education
            {
                EducationId = 1,
                City = "Ipsum City",
                Country = "Dolor Republic",
                State = "Lorem State",
                SchoolName = "Fudge School",
                YearAttended = DateTime.UtcNow,
                YearGraduated = DateTime.UtcNow,
                EducationTypeId = 1,
                EducationType = new Db.EducationType { EducationTypeId = 1, EducationTypeName = "Grade School" },
                UserId = 1,
                User = new Db.User { UserId = 1 }
            };

            var result = EducationMapper.ToDto(param);

            Assert.IsInstanceOf(typeof(Education), result);
            Assert.NotNull(result);
            Assert.NotNull(result.EducationType);
        }

        [Test]
        public void ShouldTransformEducationWithNullEducationTypeToDto()
        {
            var param = new Db.Education
            {
                EducationId = 1,
                City = "Ipsum City",
                Country = "Dolor Republic",
                State = "Lorem State",
                SchoolName = "Fudge School",
                YearAttended = DateTime.UtcNow,
                YearGraduated = DateTime.UtcNow,
                EducationTypeId = 1,
                EducationType = null,
                UserId = 1,
                User = new Db.User { UserId = 1 }
            };

            var result = EducationMapper.ToDto(param);

            Assert.IsInstanceOf(typeof(Education), result);
            Assert.NotNull(result);
            Assert.IsNull(result.EducationType);
        }

        [Test]
        public void ShouldReturnNullWhenPassedNullOnTransformEducationToDto()
        {
            var result = EducationMapper.ToDto(null);
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldTransformEducationToEntity()
        {
            var param = new Education
            {
                EducationId = 1,
                City = "Ipsum City",
                Country = "Dolor Republic",
                State = "Lorem State",
                SchoolName = "Fudge School",
                YearAttended = DateTime.UtcNow,
                YearGraduated = DateTime.UtcNow,
                EducationType = new EducationType { EducationTypeId = 1, EducationTypeName = "Grade School" },
                UserId = 1
            };

            var result = EducationMapper.ToEntity(param);

            Assert.IsInstanceOf(typeof(Db.Education), result);
            Assert.NotNull(result);
            Assert.NotNull(result.EducationType);
            Assert.AreNotEqual(0, result.EducationTypeId);
        }

        [Test]
        public void ShouldReturnNullWhenTransformEducationWithNullEducationTypeToEntity()
        {
            var param = new Education
            {
                EducationId = 1,
                City = "Ipsum City",
                Country = "Dolor Republic",
                State = "Lorem State",
                SchoolName = "Fudge School",
                YearAttended = DateTime.UtcNow,
                YearGraduated = DateTime.UtcNow,
                EducationType = null,
                UserId = 1
            };

            var result = EducationMapper.ToEntity(param);

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenPassedNullOnTransformEducationToEntity()
        {
            var result = EducationMapper.ToEntity(null);
            Assert.IsNull(result);
        }
    }
}
