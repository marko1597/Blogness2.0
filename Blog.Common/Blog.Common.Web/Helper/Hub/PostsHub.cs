using System;
using System.Collections.Generic;
using System.Configuration;
using Blog.Common.Contracts;

namespace Blog.Common.Web.Helper.Hub
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
