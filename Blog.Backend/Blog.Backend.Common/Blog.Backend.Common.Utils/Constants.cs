using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Blog.Backend.Common.Utils
{
    public static class Constants
    {
        public static int DefaultPostsThreshold = 10;
        public static double SessionValidityLength = 15.0;
        public static string FileMediaLocation = @"C:\Temp\SampleImages\";
        public static string FileMediaUrl = string.Format("https://{0}/blogapi/api/media/", new UserHelper().GetLocalIpAddress());
        public static string FileMediaThumbnailUrl = string.Format("https://{0}/blogapi/api/media/thumb/", new UserHelper().GetLocalIpAddress());
    }
}
