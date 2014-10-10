using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class HttpClientHelperTest
    {
        [Test]
        public void ShouldReturnStringOnGet()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{ Test: Message }")
            };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            var result = httpClientHelper.Get("http://localhost/", "foo");

            Assert.AreEqual("{ Test: Message }", result);
        }

        [Test]
        public void ShouldThrowExceptionWhenHttpClientFromGetResponseIsNull()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted) { Content = null };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            Assert.Throws<BlogException>(() => httpClientHelper.Get("http://localhost/", "foo"));
        }

        [Test]
        public void ShouldReturnStringOnHttpGet()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{ Test: Message }")
            };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            var result = httpClientHelper.HttpGet("http://localhost/", "foo");

            Assert.AreEqual(fakeResponse, result);
        }

        [Test]
        public void ShouldThrowExceptionWhenHttpClientFromHttpGetResponseIsNull()
        {
            var fakeHandler = new FakeHttpMessageHandler(null);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            Assert.Throws<BlogException>(() => httpClientHelper.HttpGet("http://localhost/", "foo"));
        }

        [Test]
        public void ShouldReturnStringOnPost()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{ Test: Message }")
            };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var testObj = new { Id = 1, Name = "foobar" };
            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            var result = httpClientHelper.Post("http://localhost/", "foo", testObj);

            Assert.AreEqual("{ Test: Message }", result);
        }

        [Test]
        public void ShouldThrowExceptionWhenHttpClientFromPostResponseIsNull()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted) { Content = null };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var testObj = new { Id = 1, Name = "foobar" };
            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            Assert.Throws<BlogException>(() => httpClientHelper.Post("http://localhost/", "foo", testObj));
        }

        [Test]
        public void ShouldReturnStringOnPut()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{ Test: Message }")
            };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var testObj = new { Id = 1, Name = "foobar" };
            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            var result = httpClientHelper.Put("http://localhost/", "foo", testObj);

            Assert.AreEqual("{ Test: Message }", result);
        }

        [Test]
        public void ShouldThrowExceptionWhenHttpClientFromPutResponseIsNull()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Accepted) { Content = null };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var httpClient = new HttpClient(fakeHandler) { BaseAddress = new Uri("http://localhost/") };

            var testObj = new { Id = 1, Name = "foobar" };
            var httpClientHelper = new HttpClientHelper { HttpClientObj = httpClient };
            Assert.Throws<BlogException>(() => httpClientHelper.Put("http://localhost/", "foo", testObj));
        }

        [Test]
        public void ShouldCreateHttpClientPropertyWhenItIsNull()
        {
            var httpClientHelper = new HttpClientHelper
            {
                HttpClientObj = null,
                BaseUri = "http://localhost/foo"
            };
            var result = httpClientHelper.HttpClientObj;

            Assert.NotNull(result);
            Assert.AreEqual("http://localhost/foo", result.BaseAddress.ToString());
        }

        public class FakeHttpMessageHandler : HttpMessageHandler
        {
            readonly HttpResponseMessage _response;

            public FakeHttpMessageHandler(HttpResponseMessage response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetResult(_response);
                return tcs.Task;
            }
        }
    }
}
