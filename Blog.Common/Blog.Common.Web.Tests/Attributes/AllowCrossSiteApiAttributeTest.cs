using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class AllowCrossSiteApiAttributeTest
    {
        private TestController _controller;
        private HttpConfiguration _httpConfiguration;
        private HttpRequestMessage _httpRequestMessage;
        private IHttpRoute _httpRoute;
        private HttpRouteData _httpRouteData;
        private Mock<HttpActionDescriptor> _httpActionDescriptor;

        [SetUp]
        public void TestInit()
        {
            _controller = new TestController();
            _httpConfiguration = new HttpConfiguration();
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            _httpRoute = _httpConfiguration.Routes.MapHttpRoute("test", "api/{controller}/{id}");
            _httpRouteData = new HttpRouteData(_httpRoute, new HttpRouteValueDictionary { { "controller", "test" } });
            _httpActionDescriptor = new Mock<HttpActionDescriptor>();

            _controller.ControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, _httpRequestMessage);
            _controller.Request = _httpRequestMessage;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = _httpConfiguration;
        }

        [Test]
        public void ShouldAppendAccessControlAllowOriginHeader()
        {
            var httpActionExecutedContext =
                new HttpActionExecutedContext(new HttpActionContext(_controller.ControllerContext, _httpActionDescriptor.Object), null)
                {
                    Response = new HttpResponseMessage(HttpStatusCode.Accepted)
                };

            var attribute = new AllowCrossSiteApiAttribute();
            attribute.OnActionExecuted(httpActionExecutedContext);

            var result = httpActionExecutedContext.Response.Headers.First(a => a.Key == "Access-Control-Allow-Origin").Value.FirstOrDefault();
            Assert.AreEqual("*", result);
        }

        [Test]
        public void ShouldNotAppendAccessControlAllowOriginHeaderWhenResponseNull()
        {
            var httpActionExecutedContext =
                new HttpActionExecutedContext(new HttpActionContext(_controller.ControllerContext, _httpActionDescriptor.Object), null)
                {
                    Response = null
                };

            var attribute = new AllowCrossSiteApiAttribute();
            attribute.OnActionExecuted(httpActionExecutedContext);

            var result = httpActionExecutedContext.Response;
            Assert.IsNull(result);
        }

        protected class TestController : ApiController { }
    }
}
