using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                new DummyModel { Id = 5, Name = "ipsum" },
            };
        }

        [Test]
        public void ShouldGetAllCachedItems()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetAll()).Returns(_dummyList);

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var result = cache.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ShouldGetCachedItemById()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Get(It.IsAny<int>(), It.IsAny<string>())).Returns(_dummyList[0]);

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var result = cache.Get(1, "foo");

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void ShouldGetCachedItemByName()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(_dummyList[0]);

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var result = cache.Get("foo", "bar");

            Assert.NotNull(result);
            Assert.AreEqual("foo", result.Name);
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
        public void ShouldSetAllCacheItems()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetAll(_dummyList));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.SetAll(_dummyList));
        }

        [Test]
        public void ShouldSetCacheItem()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Set(_dummyList[0]));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.Set(_dummyList[0]));
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
        public void ShouldRemoveCachedItemById()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Remove(It.IsAny<int>(), It.IsAny<string>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.Remove(1, "foo"));
        }

        [Test]
        public void ShouldRemoveCachedItemByName()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Remove(It.IsAny<string>(), It.IsAny<string>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.Remove("foo", "foo"));
        }

        [Test]
        public void ShouldReplaceCachedItem()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Replace(It.IsAny<string>(), It.IsAny<DummyModel>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.Replace("foo", _dummyList[0]));
        }

        public class DummyModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
