using Blog.Common.Utils.Helpers;

namespace Blog.Common.Utils
{
    public static class Constants
    {
        public static int DefaultPostsThreshold = 10;
        public static double SessionValidityLength = 15.0;
        public static string FileMediaLocation = @"C:\Temp\SampleImages\";
        public static string FileMediaUrl = string.Format("https://{0}/blogapi/api/media/", new UserHelper().GetLocalIpAddress());

        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public enum Error
        {
            NotLoggedIn = 1,
            InternalError = 2,
            RequestNotAllowed = 3,
            InvalidParameters = 4,
            RecordNotFound = 5,
            InvalidCredentials = 6,
            ValidationError = 7
        }
    }
}
