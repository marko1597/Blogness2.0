using System;
using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class SessionRemoteService : BaseService, ISessionService
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly IHttpClientHelper _httpClientHelper;

        public SessionRemoteService(IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
        }

        public List<Session> GetAll()
        {
            try
            {
                var sessions = JsonHelper.DeserializeJson<List<Session>>(
                _httpClientHelper.Get(_configurationHelper.GetAppSettings("BlogApi"), "session"));
                return sessions;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Session GetByUser(string username)
        {
            try
            {
                var session = JsonHelper.DeserializeJson<Session>(
                    _httpClientHelper.Get(_configurationHelper.GetAppSettings("BlogApi"), "session/" + username));
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
                    _httpClientHelper.Get(_configurationHelper.GetAppSettings("BlogApi"), "session/ip/" + ipAddress));
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
                var loggedUser = JsonHelper.DeserializeJson<LoggedUser>(
                    _httpClientHelper.Post(_configurationHelper.GetAppSettings("BlogApi"), "session?format=json", loginModel));
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
                var result = JsonHelper.DeserializeJson<Error>(
                    _httpClientHelper.Put(_configurationHelper.GetAppSettings("BlogApi"), "session", loginModel));
                return result;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
