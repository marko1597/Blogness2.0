using System;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Extensions.Elmah;

namespace Blog.Web.Api.Helper.Hub
{
    public class PostsHub
    {
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public PostsHub(IErrorSignaler errorSignaler, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _errorSignaler = errorSignaler;
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
        }

        public void PushPostLikes(PostLikesUpdate postLikesUpdate)
        {
            try
            {
                _httpClientHelper.Post(_configurationHelper.GetAppSettings("BlogRoot"), "hub/postlikesupdate?format=json", postLikesUpdate);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void PushTestMessage(string message)
        {
            try
            {
                _httpClientHelper.Post(_configurationHelper.GetAppSettings("BlogRoot"), "hub/testmessage?format=json", message);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
