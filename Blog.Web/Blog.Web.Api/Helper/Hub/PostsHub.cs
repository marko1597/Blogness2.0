using System;
using System.ComponentModel.Composition;
using System.Configuration;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Common.Web.Extensions.Elmah;

namespace Blog.Web.Api.Helper.Hub
{
    public class PostsHub
    {
        [Import]
        public IErrorSignaler ErrorSignaler { get; set; }

        public void PushPostLikes(PostLikesUpdate postLikesUpdate)
        {
            try
            {
                new HttpClientHelper(ConfigurationManager.AppSettings["BlogRoot"])
                    .Post("hub/postlikesupdate?format=json", postLikesUpdate);
            }
            catch (Exception ex)
            {
                ErrorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void PushTestMessage(string message)
        {
            try
            {
                new HttpClientHelper(ConfigurationManager.AppSettings["Blog"])
                    .Post("hub/testmessage?format=json", message);
            }
            catch (Exception ex)
            {
                ErrorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
