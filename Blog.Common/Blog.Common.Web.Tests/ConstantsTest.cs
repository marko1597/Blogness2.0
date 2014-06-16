using NUnit.Framework;

namespace Blog.Common.Web.Tests
{
    [TestFixture]
    public class ConstantsTest
    {
        [Test]
        public void ShouldGetJsonDateFormat()
        {
            Assert.AreEqual("yyyy-MM-ddThh:mm:ss.fff", Constants.JsonDateFormat);
        }
    }
}
