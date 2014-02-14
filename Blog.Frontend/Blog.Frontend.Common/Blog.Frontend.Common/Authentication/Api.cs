using System;
using System.Configuration;
using Blog.Frontend.Common.Helper;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Frontend.Common.Authentication
{
    public class Api
    {
        public Session IsLoggedIn(string username)
        {
            var session = new Session();
            try
            {
                session = JsonHelper.DeserializeJson<Session>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Get("session/" + username));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return session;
        }

        public Session GetByIp(string ipAddress)
        {
            var session = new Session();
            try
            {
                session = JsonHelper.DeserializeJson<Session>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Get("session/ip/" + ipAddress));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return session;
        }
    }
}
