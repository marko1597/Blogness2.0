using System;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.Common.Web.Authentication;
using Blog.Common.Web.Extensions.Elmah;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;

namespace Blog.Common.Web.Tests.Authentication
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class AuthenticationHelperTest
    {
        private Mock<IErrorSignaler> _errorSignaler;
        private AuthenticationHelper _authenticationHelper;

        [SetUp]
        public void TestInit()
        {
            _errorSignaler = new Mock<IErrorSignaler>();
            _errorSignaler.Setup(a => a.SignalFromCurrentContext(It.IsAny<Exception>()));

            _authenticationHelper = new AuthenticationHelper {ErrorSignaler = _errorSignaler.Object};
        }

        [Test]
        public void ShouldSignInUserWhenValid()
        {
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignIn());
            mockAuthenticationManager.Setup(am => am.SignOut());

            _authenticationHelper.AuthenticationManager = mockAuthenticationManager.Object;

            var user = new User
            {
                Error = null,
                UserName = "test",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mail.com"
            };

            var result = _authenticationHelper.SignIn(user);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void ShouldThrowExceptionWhenSignInOnAuthenticationManagerFails()
        {
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn()).Throws(new Exception());

            var user = new User
            {
                Error = null,
                UserName = "test",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mail.com"
            };

            Assert.Throws<BlogException>(() =>  _authenticationHelper.SignIn(user));
        }

        [Test]
        public void ShouldNotSignInWhenUserNull()
        {
            var result = _authenticationHelper.SignIn(null);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ShouldNotSignInWhenUserErrorNotNull()
        {
            var result = _authenticationHelper.SignIn(new User { Error = new Error() });
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ShouldSignOutSuccessfully()
        {
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());

            _authenticationHelper.AuthenticationManager = mockAuthenticationManager.Object;

            var user = new User
            {
                Error = null,
                UserName = "test",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mail.com"
            };

            var result = _authenticationHelper.SignOut(user);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void ShouldNotSignOutWhenUserNull()
        {
            var result = _authenticationHelper.SignOut(null);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ShouldNotSignOutWhenUserErrorNotNull()
        {
            var result = _authenticationHelper.SignOut(new User { Error = new Error() });
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ShouldThrowExceptionWhenSignOutOnAuthenticationManagerFails()
        {
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut()).Throws(new Exception());

            var user = new User
            {
                Error = null,
                UserName = "test",
                FirstName = "foo",
                LastName = "bar",
                EmailAddress = "test@mail.com"
            };

            Assert.Throws<BlogException>(() => _authenticationHelper.SignOut(user));
        }
    }
}
