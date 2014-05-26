using System;
using System.Collections.Generic;
using System.Configuration;
using Blog.Common.Contracts;
using Blog.Common.Contracts.Utils;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class SessionRemoteService : ISession
    {
        public List<Session> GetAll()
        {
            return SessionFactory.GetInstance().CreateSession().GetAll();
        }

        public Session GetByUser(string username)
        {
            try
            {
                var session = JsonHelper.DeserializeJson<Session>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Get("session/" + username));

                return session;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Session GetByIp(string ipAddress)
        {
            try
            {
                var session = JsonHelper.DeserializeJson<Session>(
                    new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Get("session/ip/" + ipAddress));

                return session;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            try
            {
                var loginModel = new Login { Username = userName, Password = passWord };
                var loggedUser = JsonHelper.DeserializeJson<LoggedUser>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Post("session?format=json", loginModel));
                return loggedUser;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Error Logout(string userName)
        {
            try
            {
                var loginModel = new Login { Username = userName };
                var result = JsonHelper.DeserializeJson<Error>(new HttpClientHelper(ConfigurationManager.AppSettings["BlogApi"])
                    .Put("session", loginModel));
                return result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
