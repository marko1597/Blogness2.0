using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Blog.Common.Contracts.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BaseContractTest
    {
        [Test]
        public void ShouldGenerateError()
        {
            var address = new Address().GenerateError<Address>(1, "foobar");
            Assert.AreEqual(1, address.Error.Id);
            Assert.AreEqual("foobar", address.Error.Message);
        }

        [Test]
        public void ShouldGenerateErrorWithException()
        {
            var address = new Address().GenerateError<Address>(1, "foobar", new Exception("loremipsum"));
            Assert.AreEqual(1, address.Error.Id);
            Assert.AreEqual("foobar", address.Error.Message);
            Assert.AreEqual("loremipsum", address.Error.Exception.Message);
        }
    }
}
