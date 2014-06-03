using System.Linq;
using System.Net;
using System.Net.Sockets;
using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    public class UserHelperTest
    {
        [Test]
        public void ShouldGetIpSuccessfully()
        {
            var expectedresult = "localhost";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                expectedresult = ip.ToString();
                break;
            }

            var result = UserHelper.GetLocalIpAddress();
            Assert.AreEqual(expectedresult, result);
        }
    }
}
