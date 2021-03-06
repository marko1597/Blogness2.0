﻿using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Blog.Common.Utils.Helpers
{
    public static class UserHelper
    {
        public static string GetLocalIpAddress()
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
