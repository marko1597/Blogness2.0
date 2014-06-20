using System;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Extensions.Elmah;

namespace Blog.Web.Api.Helper.Hub
{
    public class CommentsHub
    {
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public CommentsHub(IErrorSignaler errorSignaler, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _errorSignaler = errorSignaler;
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
        }

        public void PushCommentLikes(CommentLikesUpdate commentLikesUpdate)
        {
            try
            {
                _httpClientHelper.Post(_configurationHelper.GetAppSettings("BlogRoot"), "hub/commentLikesUpdate?format=json", commentLikesUpdate);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}