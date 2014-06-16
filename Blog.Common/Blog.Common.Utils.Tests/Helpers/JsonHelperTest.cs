using System.Diagnostics.CodeAnalysis;
using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class JsonHelperTest
    {
        [Test]
        public void ShouldSerializeObjectToJson()
        {
            var obj = new TestObject {Id = 1, Name = "foo" };
            const string expectedResult = "{\"Id\":1,\"Name\":\"foo\",\"Child\":null}";
            var result = JsonHelper.SerializeJson(obj);

            Assert.IsNotEmpty(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ShouldBeEmptyStringWhenObjectOnSerializeIsNull()
        {
            object testObj = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var result = JsonHelper.SerializeJson(testObj);

            Assert.IsEmpty(result);
        }

        [Test]
        public void ShouldDeserializeJsonToObject()
        {
            const string jsonObj = "{\"Id\":1,\"Name\":\"foo\"}";
            var expectedResult = new TestObject {Id = 1, Name = "foo"};
            var result = JsonHelper.DeserializeJson<TestObject>(jsonObj);

            Assert.NotNull(result);
            Assert.AreEqual(expectedResult.Id, result.Id);
            Assert.AreEqual(expectedResult.Name, result.Name);
            Assert.AreEqual(expectedResult.Child, result.Child);
        }

        [Test]
        public void ShouldBeNullWhenStringIsEmptyOnDeserializeJson()
        {
            var result = JsonHelper.DeserializeJson<TestObject>(string.Empty);
            Assert.IsNull(result);
        }

        protected class TestObject : object
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public TestObject Child { get; set; }
        }
    }
}
