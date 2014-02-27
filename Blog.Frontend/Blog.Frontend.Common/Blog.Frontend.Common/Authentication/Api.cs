using System;
using System.Configuration;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Frontend.Common.Helper;

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
                ipAddress = ipAddress.Replace(':', 'x');
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

        public LoggedUser Login(Login credentials)
        {
            var loggedUser = new LoggedUser();
            try
            {
                loggedUser = JsonHelper.DeserializeJson<LoggedUser>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                        .Post("session?format=json", credentials));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return loggedUser;
        }

        public bool Logout(Login credentials)
        {
            var isLoggedOut = false;
            try
            {
                isLoggedOut = JsonHelper.DeserializeJson<bool>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                        .Put("session", credentials));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isLoggedOut;
        }
    }
}
