using System;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Blog.Common.Web.Tests.Attributes
{
    [TestClass]
    public class ValidateUsernameAttributeTest
    {
        private Mock<IUsersResource> _userResource;

        [SetUp]
        public void TestInit()
        {
            _userResource = new Mock<IUsersResource>();
        }

        [Test]
        public void ShouldReturnTrueWhenUsernameIsValid()
        {
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>()))
                .Returns(new User { Error = new Error { Id = (int)Utils.Constants.Error.RecordNotFound } });

            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = attr.IsValid("foo");

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenUserReturnedIsNull()
        {
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>()))
                .Returns((User)null);

            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = attr.IsValid("foo");

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnFalseWhenUserErrorIsNull()
        {
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>()))
                .Returns(new User());

            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = attr.IsValid("foo");

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnFalseWhenUserErrorIdIsNotRecordNotFound()
        {
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>()))
                .Returns(new User { Error = new Error { Id = (int)Utils.Constants.Error.InternalError } });

            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = attr.IsValid("foo");

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenUsernameIsNotString()
        {
            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<BlogException>(() => attr.IsValid(13));

            Assert.AreEqual("Model property is not a string.", result.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenUsernameIsEmptyOrNull()
        {
            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<BlogException>(() => attr.IsValid(string.Empty));

            Assert.AreEqual("Model property is null/empty.", result.Message);
        }

        [Test]
        public void ShouldReturnFalseWhenFetchingUserFails()
        {
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>()))
                .Throws(new Exception());

            var attr = new ValidateUsernameAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<BlogException>(() => attr.IsValid("foo"));

            Assert.IsInstanceOf(typeof(BlogException), result);
        }
    }
}
