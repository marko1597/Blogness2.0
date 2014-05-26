using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Common.Web.Attributes;
using Moq;
using NUnit.Framework;

namespace Blog.Common.Web.Tests.Attributes
{
    [TestFixture]
    public class AllowCrossSiteAttributeTest
    {
        [Test]
        public void ShouldAppendAccessControlAllowOriginHeader()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(r => r.HttpMethod).Returns("GET");
            request.SetupGet(r => r.Url).Returns(new Uri("http://localhost/test"));

            var response = new Mock<HttpResponseBase>();
            response.SetupGet(r => r.StatusCode).Returns(200);
            response.SetupGet(r => r.Headers).Returns(new WebHeaderCollection());

            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(c => c.Request).Returns(request.Object);
            httpContext.SetupGet(c => c.Response).Returns(response.Object);

            var actionExecutingContext = new Mock<ActionExecutingContext>();
            actionExecutingContext.SetupGet(c => c.HttpContext).Returns(httpContext.Object);

            var attribute = new AllowCrossSiteAttribute();
            attribute.OnActionExecuting(actionExecutingContext.Object);

            var result = actionExecutingContext.Object.RequestContext.HttpContext.Response.Headers.GetValues("Access-Control-Allow-Origin");

            Assert.NotNull(result);
            Assert.AreEqual("*", result[0]);
        }
    }
}
