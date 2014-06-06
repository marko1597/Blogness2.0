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
    public class HobbyLogicTest
    {
        private Mock<IHobbyRepository> _hobbyRepository;

        private HobbyLogic _hobbyLogic;

        private List<Hobby> _hobbies;

        [SetUp]
        public void TestInit()
        {
            #region Hobbies

            _hobbies = new List<Hobby>
                     {
                         new Hobby
                         {
                             HobbyId = 1,
                             HobbyName = "Fooing",
                             UserId = 1,
                             User = new User
                             {
                                 UserId = 1,
                                 UserName = "foo"
                             }
                         },
                         new Hobby
                         {
                             HobbyId = 2,
                             HobbyName = "Barring",
                             UserId = 1,
                             User = new User
                             {
                                 UserId = 1,
                                 UserName = "foo"
                             }
                         },
                         new Hobby
                         {
                             HobbyId = 3,
                             HobbyName = "Bazzing",
                             UserId = 2,
                             User = new User
                             {
                                 UserId = 1,
                                 UserName = "bar"
                             }
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetHobbiesByUser()
        {
            var expected = _hobbies.Where(a => a.UserId == 1).ToList();
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Hobby, bool>>>(), false))
                .Returns(expected);

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            var results = _hobbyLogic.GetByUser(1);

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetHobbiesByUserFails()
        {
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Hobby, bool>>>(), true))
                .Throws(new Exception());

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);
            Assert.Throws<BlogException>(() => _hobbyLogic.GetByUser(1));
        }

        [Test]
        public void ShouldAddHobby()
        {
            var dbResult = new Hobby
            {
                HobbyId = 4,
                HobbyName = "Fudging",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "foo"
                }
            };
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Add(It.IsAny<Hobby>())).Returns(dbResult);

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            var result = _hobbyLogic.Add(new Common.Contracts.Hobby
            {
                HobbyId = 4,
                HobbyName = "Fudging",
                UserId = 1
            });

            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddHobbyFails()
        {
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Add(It.IsAny<Hobby>())).Throws(new Exception());

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            Assert.Throws<BlogException>(() => _hobbyLogic.Add(new Common.Contracts.Hobby()));
        }

        [Test]
        public void ShouldUpdateHobby()
        {
            var dbResult = new Hobby
            {
                HobbyId = 4,
                HobbyName = "Fudging",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "foo"
                }
            };
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Edit(It.IsAny<Hobby>())).Returns(dbResult);

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            var result = _hobbyLogic.Update(new Common.Contracts.Hobby
            {
                HobbyId = 4,
                HobbyName = "Fudging",
                UserId = 1
            });

            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdateHobbyFails()
        {
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Edit(It.IsAny<Hobby>())).Throws(new Exception());

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            Assert.Throws<BlogException>(() => _hobbyLogic.Update(new Common.Contracts.Hobby()));
        }

        [Test]
        public void ShouldReturnTrueOnDeleteHobby()
        {
            var dbResult = new List<Hobby> { new Hobby { HobbyId = 1 } };
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Hobby, bool>>>(), false))
               .Returns(dbResult);

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            var result = _hobbyLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeleteHobbyFoundNoRecord()
        {
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Hobby, bool>>>(), false))
               .Returns(new List<Hobby>());

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            var result = _hobbyLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteHobbyFails()
        {
            _hobbyRepository = new Mock<IHobbyRepository>();
            _hobbyRepository.Setup(a => a.Delete(It.IsAny<Hobby>())).Throws(new Exception());

            _hobbyLogic = new HobbyLogic(_hobbyRepository.Object);

            Assert.Throws<BlogException>(() => _hobbyLogic.Delete(1));
        }
    }
}
