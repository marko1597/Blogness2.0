using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Blog.Common.Web.Attributes;
using Moq;
using NUnit.Framework;

namespace Blog.Common.Web.Tests.Attributes
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BlogApiAuthorizationAttributeTest
    {
        private TestController _controller;
        private HttpConfiguration _httpConfiguration;
        private HttpRequestMessage _httpRequestMessage;
        private IHttpRoute _httpRoute;
        private HttpRouteData _httpRouteData;
        private HttpActionContext _httpActionContext;
        private Mock<IPrincipal> _principal;
        private Mock<HttpActionDescriptor> _httpActionDescriptor;

        [SetUp]
        public void TestInit()
        {
            _principal = new Mock<IPrincipal>();
            _httpActionDescriptor = new Mock<HttpActionDescriptor>();

            _controller = new TestController();
            _httpConfiguration = new HttpConfiguration();
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            _httpRoute = _httpConfiguration.Routes.MapHttpRoute("test", "api/{controller}/{id}");
            _httpRouteData = new HttpRouteData(_httpRoute, new HttpRouteValueDictionary { { "controller", "test" } });

            _controller.ControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, _httpRequestMessage);
            _controller.Request = _httpRequestMessage;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = _httpConfiguration;

            _httpActionContext = new HttpActionContext(_controller.ControllerContext, _httpActionDescriptor.Object);
        }

        [Test]
        public void ShouldSuccessfullyAuthenticate()
        {
            _principal.SetupGet(x => x.Identity.IsAuthenticated).Returns(true);
            _principal.SetupGet(x => x.Identity.Name).Returns("foo");
            var httpAuthenticationContext = new HttpAuthenticationContext(_httpActionContext, _principal.Object);

            var attribute = new BlogApiAuthorizationAttribute();
            attribute.OnAuthentication(httpAuthenticationContext);

            Assert.IsNull(httpAuthenticationContext.ErrorResult);
        }

        [Test]
        public void ShouldReturnUnauthorizedWhenNotAuthenticated()
        {
            _principal.SetupGet(x => x.Identity.IsAuthenticated).Returns(false);
            _principal.SetupGet(x => x.Identity.Name).Returns("foo");
            var httpAuthenticationContext = new HttpAuthenticationContext(_httpActionContext, _principal.Object);

            var attribute = new BlogApiAuthorizationAttribute();
            attribute.OnAuthentication(httpAuthenticationContext);

            Assert.IsNotNull(httpAuthenticationContext.ErrorResult);
        }

        protected class TestController : ApiController { }
    }
}
