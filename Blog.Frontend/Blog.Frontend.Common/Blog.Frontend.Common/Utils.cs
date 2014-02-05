using System;
using System.Text.RegularExpressions;

namespace Blog.Frontend.Common
{
    public static class Utils
    {
        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static bool IsGuid(string candidate)
        {
            if(candidate != null)
            {
                return isGuid.IsMatch(candidate);
            }
            return false;
        }

        public static string GenerateImagePath(int id, string storageRoot)
        {
            return storageRoot + id + "\\" + Guid.NewGuid() + "\\";
        }
    }
}
