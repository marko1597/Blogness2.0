using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Interfaces;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Blog.Common.Web.Tests.Attributes
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PreventCrossUserManipulationTest
    {
        private TestController _controller;
        private HttpConfiguration _httpConfiguration;
        private HttpRequestMessage _httpRequestMessage;
        private IHttpRoute _httpRoute;
        private HttpRouteData _httpRouteData;
        private HttpActionContext _httpActionContext;
        private Mock<IUsersResource> _userResource;
        private Mock<HttpActionDescriptor> _httpActionDescriptor;

        [SetUp]
        public void TestInit()
        {
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

            _userResource = new Mock<IUsersResource>();
        }

        [Test]
        public void ShouldNotThrowWhenUserAllowed()
        {
            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns(new User { Id = 1 });
            _httpActionContext.ActionArguments.Add("dummy", new DummyObject { User = new User { Id = 1 } });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };

            Assert.DoesNotThrow(() => attribute.OnActionExecuting(_httpActionContext));
        }

        [Test]
        public void ShouldSuccessWhenParameterIsUserAndUserIsAllowed()
        {
            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns(new User { Id = 1 });
            _httpActionContext.ActionArguments.Add("dummy", new User { Id = 1 });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };

            Assert.DoesNotThrow(() => attribute.OnActionExecuting(_httpActionContext));
        }

        [Test]
        public void ShouldSuccessfullyGetUserIdInUserObject()
        {
            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns(new User { Id = 1 });
            _httpActionContext.ActionArguments.Add("dummy", new User { Id = 1 });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };

            Assert.DoesNotThrow(() => attribute.OnActionExecuting(_httpActionContext));
        }

        [Test]
        public void ShouldSuccessfullyGetUserIdInComplexObjectParameter()
        {
            var complexDummyObject = new DummyComplexObject
                                     {
                                         SomeValue = 1,
                                         DummyObject = new DummyObject
                                                       {
                                                           Name = "foobar",
                                                           User = new User
                                                                  {
                                                                      Id = 1
                                                                  }
                                                       }
                                     };

            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns(new User { Id = 1 });
            _httpActionContext.ActionArguments.Add("dummy", complexDummyObject);

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };

            Assert.DoesNotThrow(() => attribute.OnActionExecuting(_httpActionContext));
        }

        [Test]
        public void ShouldSuccessfullyUseUserIdInParameter()
        {
            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns(new User { Id = 1 });
            _httpActionContext.ActionArguments.Add("dummy", new UserIdOnlyDummyObject { UserId = 1 });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };

            Assert.DoesNotThrow(() => attribute.OnActionExecuting(_httpActionContext));
        }

        [Test]
        public void ShouldThrowForbiddenWhenUserNotAllowed()
        {
            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns(new User { Id = 2 });
            _httpActionContext.ActionArguments.Add("dummy", new DummyObject { User = new User { Id = 1 } });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.Forbidden, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenNoParameterIsUsed()
        {
            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenParameterUsedIsNull()
        {
            _httpActionContext.ActionArguments.Add("dummy", null);

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenUserPropertyNotPresentInParameter()
        {
            _httpActionContext.ActionArguments.Add("dummy", new NoUserDummyObject { Name = "foo" });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenUserPropertyIsEmptyInParameter()
        {
            _httpActionContext.ActionArguments.Add("dummy", new DummyObject { Name = "foo" });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenIdInUserPropertyIsZeroInParameter()
        {
            _httpActionContext.ActionArguments.Add("dummy", new DummyObject { User = new User { Id = 0 } });

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenNameIsEmptyInPrincipal()
        {
            _httpActionContext.ActionArguments.Add("dummy", new DummyObject { User = new User { Id = 1 } });
            _controller.ControllerContext.RequestContext.Principal = 
                new GenericPrincipal(new GenericIdentity("", ""), null);

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        [Test]
        public void ShouldThrowWhenFailedToFetchUser()
        {
            _httpActionContext.ActionArguments.Add("dummy", new DummyObject { User = new User { Id = 1 } });
            _controller.ControllerContext.RequestContext.Principal =
                new GenericPrincipal(new GenericIdentity("foo", "bar"), new[] { "user" });
            _userResource.Setup(a => a.GetByUserName(It.IsAny<string>())).Returns((User)null);

            var attribute = new PreventCrossUserManipulationAttribute { UsersResource = _userResource.Object };
            var result = Assert.Throws<HttpResponseException>(() => attribute.OnActionExecuting(_httpActionContext));

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
        }

        protected class DummyComplexObject
        {
            public DummyObject DummyObject { get; set; }
            public int SomeValue { get; set; }
        }

        protected class DummyObject
        {
            public string Name { get; set; }
            public User User { get; set; }
        }

        protected class UserIdOnlyDummyObject
        {
            public int UserId { get; set; }
        }

        protected class NoUserDummyObject
        {
            public string Name { get; set; }
        }

        protected class TestController : ApiController { }
    }
}
