using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Utils.Extensions;
using Blog.Logic.Caching.DataSource;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Caching.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CacheTest
    {
        private Mock<ICacheDataSource<DummyModel>> _cacheDataSource;
        private List<DummyModel> _dummyList = new List<DummyModel>();

        [SetUp]
        public void TestSetup()
        {
            _dummyList = new List<DummyModel>
            {
                new DummyModel { Id = 1, Name = "foo" },
                new DummyModel { Id = 2, Name = "bar" },
                new DummyModel { Id = 3, Name = "baz" },
                new DummyModel { Id = 4, Name = "lorem" },
                new DummyModel { Id = 5, Name = "ipsum" }
            };
        }

        [Test]
        public void ShouldGetAllCachedItems()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetList()).Returns(_dummyList);

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var result = cache.GetList();

            Assert.NotNull(result);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetAllCachedItemsFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetList()).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.GetList());
        }

        [Test]
        public void ShouldGetCacheKeys()
        {
            var keys = new List<string> { "foo", "bar", "baz" };

            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetKeys()).Returns(keys);

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var result = cache.GetKeys();

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCacheKeysFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetKeys()).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.GetKeys());
        }

        [Test]
        public void ShouldSetList()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetList(_dummyList));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.SetList(_dummyList));
        }

        [Test]
        public void ShouldThrowExceptionWhenSetListFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetList(_dummyList)).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.SetList(_dummyList));
        }
        
        [Test]
        public void ShouldRemoveAllCachedItems()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.RemoveAll());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(cache.RemoveAll);
        }

        [Test]
        public void ShouldThrowExceptionWhenRemoveAllCachedItemsFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.RemoveAll()).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(cache.RemoveAll);
        }
        
        public class DummyModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
