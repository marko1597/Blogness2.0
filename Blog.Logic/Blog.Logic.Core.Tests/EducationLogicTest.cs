using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    public class EducationLogicTest
    {
        private Mock<IEducationRepository> _educationRepository;

        private EducationLogic _educationLogic;

        private List<Education> _educations;

        [SetUp]
        public void TestInit()
        {
            #region Educations

            _educations = new List<Education>
                     {
                         new Education
                         {
                             EducationId = 1,
                             EducationType = new EducationType(),
                             City = "Foo",
                             State = "Bar",
                             Country = "Baz",
                             Course = "Fudge",
                             UserId = 1
                         },
                         new Education
                         {
                             EducationId = 2,
                             EducationType = new EducationType(),
                             City = "Lorem",
                             State = "Ipsum",
                             Country = "Dolor",
                             Course = "Sit Amet",
                             UserId = 1
                         },
                         new Education
                         {
                             EducationId = 3,
                             EducationType = new EducationType(),
                             City = "Stack",
                             State = "Pepper",
                             Country = "Cheese",
                             Course = "Pizza",
                             UserId = 2
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetEducationByUser()
        {
            var expected = _educations.Where(a => a.UserId == 1).ToList();
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), true))
                .Returns(expected);

            _educationLogic = new EducationLogic(_educationRepository.Object);

            var results = _educationLogic.GetByUser(1);

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetEducationByUserFails()
        {
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), true))
                .Throws(new Exception());

            _educationLogic = new EducationLogic(_educationRepository.Object);
            Assert.Throws<BlogException>(() => _educationLogic.GetByUser(1));
        }

        [Test]
        public void ShouldAddEducation()
        {
            var dbResult = new Education
            {
                EducationId = 4,
                EducationType = new EducationType(),
                City = "Foo",
                State = "Bar",
                Country = "Baz",
                Course = "Fudge",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "FooBar"
                }
            };
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Add(It.IsAny<Education>())).Returns(dbResult);

            _educationLogic = new EducationLogic(_educationRepository.Object);

            var result = _educationLogic.Add(new Common.Contracts.Education
            {
                EducationId = 4,
                EducationType = new Common.Contracts.EducationType(),
                City = "Foo",
                State = "Bar",
                Country = "Baz",
                Course = "Fudge",
                UserId = 1
            });

            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddEducationFails()
        {
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Add(It.IsAny<Education>())).Throws(new Exception());

            _educationLogic = new EducationLogic(_educationRepository.Object);

            Assert.Throws<BlogException>(() => _educationLogic.Add(new Common.Contracts.Education()));
        }

        [Test]
        public void ShouldUpdateEducation()
        {
            var dbResult = new Education
            {
                EducationId = 4,
                EducationType = new EducationType(),
                City = "Foo",
                State = "Bar",
                Country = "Baz",
                Course = "Fudge",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "FooBar"
                }
            };
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Edit(It.IsAny<Education>())).Returns(dbResult);

            _educationLogic = new EducationLogic(_educationRepository.Object);

            var result = _educationLogic.Update(new Common.Contracts.Education
            {
                EducationId = 4,
                EducationType = new Common.Contracts.EducationType(),
                City = "Foo",
                State = "Bar",
                Country = "Baz",
                Course = "Fudge",
                UserId = 1
            });

            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdateEducationFails()
        {
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Edit(It.IsAny<Education>())).Throws(new Exception());

            _educationLogic = new EducationLogic(_educationRepository.Object);

            Assert.Throws<BlogException>(() => _educationLogic.Update(new Common.Contracts.Education()));
        }

        [Test]
        public void ShouldReturnTrueOnDeleteEducation()
        {
            var dbResult = new List<Education> { new Education { EducationId = 1 } };
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), false))
               .Returns(dbResult);

            _educationLogic = new EducationLogic(_educationRepository.Object);

            var result = _educationLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeleteEducationFoundNoRecord()
        {
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Education, bool>>>(), false))
               .Returns(new List<Education>());

            _educationLogic = new EducationLogic(_educationRepository.Object);

            var result = _educationLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteEducationFails()
        {
            _educationRepository = new Mock<IEducationRepository>();
            _educationRepository.Setup(a => a.Delete(It.IsAny<Education>())).Throws(new Exception());

            _educationLogic = new EducationLogic(_educationRepository.Object);

            Assert.Throws<BlogException>(() => _educationLogic.Delete(1));
        }
    }
}
