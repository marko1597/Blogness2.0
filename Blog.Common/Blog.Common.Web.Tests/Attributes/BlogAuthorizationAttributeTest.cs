using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Blog.Common.Web.Attributes;
using Moq;
using NUnit.Framework;

namespace Blog.Common.Web.Tests.Attributes
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BlogAuthorizationAttributeTest
    {
        private Mock<HttpRequestBase> _requestBase;
        private Mock<HttpResponseBase> _responseBase;
        private Mock<IPrincipal> _principal;
        private Mock<HttpContextBase> _httpContextBase;
        private Mock<AuthenticationContext> _authenticationContext;

        [SetUp]
        public void TestInit()
        {
            _requestBase = new Mock<HttpRequestBase>();
            _requestBase.SetupGet(r => r.HttpMethod).Returns("GET");
            _requestBase.SetupGet(r => r.Url).Returns(new Uri("http://localhost/test"));

            _responseBase = new Mock<HttpResponseBase>();
            _responseBase.SetupGet(r => r.StatusCode).Returns(200);

            _principal = new Mock<IPrincipal>();
            _principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            _principal.SetupGet(x => x.Identity.Name).Returns("foo");

            _httpContextBase = new Mock<HttpContextBase>();
            _httpContextBase.SetupGet(c => c.Request).Returns(_requestBase.Object);
            _httpContextBase.SetupGet(c => c.Response).Returns(_responseBase.Object);
            _httpContextBase.Setup(c => c.User).Returns(_principal.Object);

            _authenticationContext = new Mock<AuthenticationContext>();
            _authenticationContext.SetupGet(c => c.HttpContext).Returns(_httpContextBase.Object);
        }

        [Test]
        public void ShouldReturnNullWhenUserAuthenticated()
        {
            _requestBase.SetupGet(r => r.IsAuthenticated).Returns(true);

            var attribute = new BlogAuthorizationAttribute();
            attribute.OnAuthentication(_authenticationContext.Object);

            Assert.IsNull(_authenticationContext.Object.Result);
        }

        [Test]
        public void ShouldReturnUnauthorizedWhenNotAuthenticated()
        {
            _requestBase.SetupGet(r => r.IsAuthenticated).Returns(false);

            var attribute = new BlogAuthorizationAttribute();
            attribute.OnAuthentication(_authenticationContext.Object);

            Assert.IsInstanceOf(typeof(HttpUnauthorizedResult), _authenticationContext.Object.Result);
        }
        
        [Test]
        public void ShouldDoNothingOnAuthenticationChallenge()
        {
            var authChallengeContext = new Mock<AuthenticationChallengeContext>();
            var attribute = new BlogAuthorizationAttribute();
            
            Assert.DoesNotThrow(() => attribute.OnAuthenticationChallenge(authChallengeContext.Object));
        }
    }
}
