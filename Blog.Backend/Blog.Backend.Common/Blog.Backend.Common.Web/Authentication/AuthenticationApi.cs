using System;
using System.Configuration;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Frontend.Common.Helper;

namespace Blog.Backend.Common.Web.Authentication
{
    public class AuthenticationApi
    {
        public bool IsUserAllowedAccess(string username, int postId)
        {
            var isAllowed = false;
            try
            {
                var post = JsonHelper.DeserializeJson<Post>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Get("posts/" + postId));

                isAllowed = username == post.User.UserName;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return isAllowed;
        }

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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return isLoggedOut;
        }
    }
}
