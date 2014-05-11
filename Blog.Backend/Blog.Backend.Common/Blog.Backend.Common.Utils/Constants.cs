using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Blog.Backend.Common.Utils
{
    public static class Constants
    {
        public static double SessionValidityLength = 15.0;
        public static string FileMediaLocation = @"C:\SampleImages\";
        public static string FileMediaUrl = string.Format("https://{0}/blogapi/api/media/", GetLocalIpAddress());
        public static string FileMediaThumbnailUrl = string.Format("https://{0}/blogapi/api/media/thumb/", GetLocalIpAddress());

        private static string GetLocalIpAddress()
        {
            var localIp = "localhost";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                localIp = ip.ToString();
                break;
            }
            return localIp;
        }
    }
}
