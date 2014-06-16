using System.Diagnostics.CodeAnalysis;
using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ConfigurationHelperTest
    {
        [Test]
        public void ShouldGetCorrectKeyInAppSettings()
        {
            var configurationHelper = new ConfigurationHelper();
            var result = configurationHelper.GetAppSettings("foo");

            Assert.AreEqual("bar", result);
        }

        [Test]
        public void ShouldReturnNullWhenKeyInAppSettingsIsMissing()
        {
            var configurationHelper = new ConfigurationHelper();
            var result = configurationHelper.GetAppSettings("bar");

            Assert.IsNull(result);
        }
    }
}
