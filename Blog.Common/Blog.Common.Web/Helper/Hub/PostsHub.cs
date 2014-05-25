﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Common.Web.Helper.Hub
{
    public class PostsHub
    {
        public void PushPostLikes(PostLikesUpdate postLikesUpdate)
        {
            try
            {
                new HttpClientHelper(ConfigurationManager.AppSettings["Blog"])
                    .Post("hub/postlikesupdate?format=json", postLikesUpdate);
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
