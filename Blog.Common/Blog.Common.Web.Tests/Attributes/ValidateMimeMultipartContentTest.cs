using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Blog.Common.Web.Attributes;
using Moq;
using NUnit.Framework;

namespace Blog.Common.Web.Tests.Attributes
{
    [TestFixture]
    public class ValidateMimeMultipartContentFilterTest
    {
        private TestController _controller;
        private HttpConfiguration _httpConfiguration;
        private HttpRequestMessage _httpRequestMessage;
        private IHttpRoute _httpRoute;
        private HttpRouteData _httpRouteData;
        private Mock<HttpActionDescriptor> _httpActionDescriptor;
        private HttpActionContext _httpActionContext;

        [SetUp]
        public void TestInit()
        {
            _controller = new TestController();
            
            _httpConfiguration = new HttpConfiguration();
            _httpRoute = _httpConfiguration.Routes.MapHttpRoute("test", "api/{controller}/{id}");
            _httpRouteData = new HttpRouteData(_httpRoute, new HttpRouteValueDictionary { { "controller", "test" } });
            
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            _httpRequestMessage.Content = new HttpMessageContent(_httpRequestMessage);

            _controller.ControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, _httpRequestMessage);
            _controller.Request = _httpRequestMessage;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = _httpConfiguration;

            _httpActionDescriptor = new Mock<HttpActionDescriptor>();
            _httpActionContext = new HttpActionContext(_controller.ControllerContext, _httpActionDescriptor.Object);
        }

        [Test]
        public void ShouldReturnNullWhenValidMultipartContent()
        {
            _httpRequestMessage.Content = new MultipartFormDataContent("----foo---bar");
            var validate = new ValidateMimeMultipartContentFilter();

            Assert.DoesNotThrow(() => validate.OnActionExecuting(_httpActionContext));
        }

        [Test]
        public void ShouldThrowExceptionWhenInvalidMultipartContent()
        {
            var validate = new ValidateMimeMultipartContentFilter();

            Assert.Throws<HttpResponseException>(() => validate.OnActionExecuting(_httpActionContext));
        }

        protected class TestController : ApiController { }
    }
}
