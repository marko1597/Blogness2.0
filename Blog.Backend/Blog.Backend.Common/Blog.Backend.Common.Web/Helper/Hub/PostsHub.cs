using System;
using System.Collections.Generic;
using System.Configuration;
using Blog.Backend.Common.Contracts;
using Blog.Frontend.Common.Helper;

namespace Blog.Backend.Common.Web.Helper.Hub
{
    public class PostsHub
    {
        public void PushPostLikes(List<PostLike> postLikes)
        {
            try
            {
                new HttpClientHelper(ConfigurationManager.AppSettings["Blog"])
                    .Post("hub/postlikesupdate?format=json", postLikes);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
