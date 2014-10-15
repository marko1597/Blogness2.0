using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [ExcludeFromCodeCoverage]
    public class ViewCountLogicTest
    {
        private Mock<IViewCountRepository> _viewCountRepository;

        private ViewCountLogic _viewCountLogic;

        private List<ViewCount> _viewCount;

        [SetUp]
        public void TestInit()
        {
            #region View Count

            _viewCount = new List<ViewCount>
                     {
                         new ViewCount
                         {
                             Id = 1,
                             PostId = 1,
                             UserId = 1
                         },
                         new ViewCount
                         {
                             Id = 2,
                             PostId = 1,
                             UserId = 2
                         },
                         new ViewCount
                         {
                             Id = 3,
                             PostId = 2,
                             UserId = 1
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetViewCounts()
        {
            var viewCount = _viewCount.Where(a => a.PostId == 1).ToList();
            _viewCountRepository = new Mock<IViewCountRepository>();
            _viewCountRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ViewCount, bool>>>(), true))
                .Returns(viewCount);

            _viewCountLogic = new ViewCountLogic(_viewCountRepository.Object);

            var result = _viewCountLogic.Get(1);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].PostId);
            Assert.AreEqual(1, result[1].PostId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetViewCountFoundNoRecords()
        {
            _viewCountRepository = new Mock<IViewCountRepository>();
            _viewCountRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ViewCount, bool>>>(), true))
                .Returns(new List<ViewCount>());

            _viewCountLogic = new ViewCountLogic(_viewCountRepository.Object);

            var result = _viewCountLogic.Get(1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetViewCountFails()
        {
            _viewCountRepository = new Mock<IViewCountRepository>();
            _viewCountRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ViewCount, bool>>>(), true))
                .Throws(new Exception());

            _viewCountLogic = new ViewCountLogic(_viewCountRepository.Object);

            Assert.Throws<BlogException>(() => _viewCountLogic.Get(1));
        }

        [Test]
        public void ShouldAddViewCount()
        {
            var viewCount = new ViewCount
            {
                Id = 4,
                PostId = 1,
                UserId = 1
            };

            _viewCountRepository = new Mock<IViewCountRepository>();
            _viewCountRepository.Setup(a => a.Add(It.IsAny<ViewCount>()))
                .Returns(viewCount);

            _viewCountLogic = new ViewCountLogic(_viewCountRepository.Object);

            var result = _viewCountLogic.Add(new Common.Contracts.ViewCount
            {
                PostId = 1,
                UserId = 1
            });

            Assert.NotNull(result);
            Assert.AreEqual(4, result.Id);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddViewCountFails()
        {
            _viewCountRepository = new Mock<IViewCountRepository>();
            _viewCountRepository.Setup(a => a.Add(It.IsAny<ViewCount>())).Throws(new Exception());

            _viewCountLogic = new ViewCountLogic(_viewCountRepository.Object);

            Assert.Throws<BlogException>(() => _viewCountLogic.Add(new Common.Contracts.ViewCount()));
        }
    }
}
