using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        public void ShouldThrowExceptionGetCachedItemById()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Get(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.Get(1, "foo"));
        }

        [Test]
        public void ShouldGetCachedListByKey()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetListByKey(It.IsAny<string>())).Returns(_dummyList);
            
            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var result = cache.GetListByKey("foo");

            Assert.NotNull(result);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionGetCachedListByKeyFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetListByKey(It.IsAny<string>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.GetListByKey("foo"));
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
        public void ShouldThrowExceptionGetCachedItemByNameFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Get(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.Get("foo", "bar"));
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
        public void ShouldSetAllCacheItems()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetAll(_dummyList));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.SetAll(_dummyList));
        }

        [Test]
        public void ShouldThrowExceptionWhenSetAllCacheItemsFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetAll(_dummyList)).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.SetAll(_dummyList));
        }

        [Test]
        public void ShouldSetCacheListByKey()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetListByKey(It.IsAny<string>(), It.IsAny<List<DummyModel>>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.SetListByKey("foo", _dummyList));
        }

        [Test]
        public void ShouldThrowExceptionWhenSetCacheListByKeyFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.SetListByKey(It.IsAny<string>(), It.IsAny<List<DummyModel>>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.SetListByKey("foo", _dummyList));
        }

        [Test]
        public void ShouldSetItemAndUpdateListWithoutOrder()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetListByKey(It.IsAny<string>())).Returns(_dummyList);
            _cacheDataSource.Setup(a => a.SetListByKey(It.IsAny<string>(), It.IsAny<List<DummyModel>>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var dummyModel = new DummyModel {Id = 0, Name = "last"};
            var result = cache.SetItemAndUpdateList("foo", dummyModel);

            Assert.NotNull(result);
            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(0, result.Last().Id);
            Assert.AreEqual("last", result.Last().Name);
        }

        [Test]
        public void ShouldReturnNullWhenSetItemAndUpdateListWithOrderReturnedEmptyCacheList()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetListByKey(It.IsAny<string>())).Returns(new List<DummyModel>());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var dummyModel = new DummyModel { Id = 0, Name = "foo" };
            var result = cache.SetItemAndUpdateList("foo", dummyModel);

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenSetItemAndUpdateListWithOrderReturnedNullCacheList()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetListByKey(It.IsAny<string>())).Returns((List<DummyModel>)null);

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var dummyModel = new DummyModel { Id = 0, Name = "foo" };
            var result = cache.SetItemAndUpdateList("foo", dummyModel);

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldSetItemAndUpdateListWithOrder()
        {
            var orderedDummyList = new List<DummyModel>
            {
                new DummyModel { Id = 0, Name = "first" },
                new DummyModel { Id = 1, Name = "foo" },
                new DummyModel { Id = 2, Name = "bar" },
                new DummyModel { Id = 3, Name = "baz" },
                new DummyModel { Id = 4, Name = "lorem" },
                new DummyModel { Id = 5, Name = "ipsum" }
            };
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.SetupSequence(a => a.GetListByKey(It.IsAny<string>()))
                .Returns(_dummyList)
                .Returns(orderedDummyList);
            _cacheDataSource.Setup(a => a.SetListByKey(It.IsAny<string>(), It.IsAny<List<DummyModel>>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var dummyModel = new DummyModel { Id = 0, Name = "first" };
            var result = cache.SetItemAndUpdateList("foo", dummyModel, a => a.OrderBy(b => b.Id));

            Assert.NotNull(result);
            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(0, result.First().Id);
            Assert.AreEqual("first", result.First().Name);
        }

        [Test]
        public void ShouldThrowExceptionWhenSetItemAndUpdateListFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.GetListByKey(It.IsAny<string>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);
            var dummyModel = new DummyModel { Id = 0, Name = "last" };

            Assert.Throws<BlogException>(() => cache.SetItemAndUpdateList("foo", dummyModel));
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
        public void ShouldThrowExceptionWhenSetCacheItemFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Set(_dummyList[0])).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.Set(_dummyList[0]));
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

        [Test]
        public void ShouldRemoveCachedItemById()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Remove(It.IsAny<int>(), It.IsAny<string>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.Remove(1, "foo"));
        }

        [Test]
        public void ShouldThrowExceptionWhenRemoveCachedItemByIdFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Remove(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.Remove(1, "foo"));
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
        public void ShouldThrowExceptionWhenRemoveCachedItemByNameFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Remove(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.Remove("foo", "foo"));
        }

        [Test]
        public void ShouldReplaceCachedItem()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Replace(It.IsAny<string>(), It.IsAny<DummyModel>()));

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.DoesNotThrow(() => cache.Replace("foo", _dummyList[0]));
        }

        [Test]
        public void ShouldThrowExceptionWhenReplaceCachedItemFails()
        {
            _cacheDataSource = new Mock<ICacheDataSource<DummyModel>>();
            _cacheDataSource.Setup(a => a.Replace(It.IsAny<string>(), It.IsAny<DummyModel>())).Throws(new Exception());

            var cache = new Cache<DummyModel>(_cacheDataSource.Object);

            Assert.Throws<BlogException>(() => cache.Replace("foo", _dummyList[0]));
        }

        public class DummyModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
