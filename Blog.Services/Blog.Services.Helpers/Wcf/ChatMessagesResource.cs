﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class ChatMessagesResource : IChatMessagesResource
    {
        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IChatMessagesService>("ChatMessagesService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }

        public ChatMessagesList GetChatMessagesListByUserId(int userId)
        {
            using (var svc = new ServiceProxyHelper<IChatMessagesService>("ChatMessagesService"))
            {
                return svc.Proxy.GetChatMessagesListByUserId(userId);
            }
        }

        public ChatMessagesList GetChatMessagesListByUsername(string username)
        {
            using (var svc = new ServiceProxyHelper<IChatMessagesService>("ChatMessagesService"))
            {
                return svc.Proxy.GetChatMessagesListByUsername(username);
            }
        }

        public List<ChatMessage> GetChatMessagesByUserIds(int fromUserId, int toUserId)
        {
            using (var svc = new ServiceProxyHelper<IChatMessagesService>("ChatMessagesService"))
            {
                return svc.Proxy.GetChatMessagesByUserIds(fromUserId, toUserId);
            }
        }

        public List<ChatMessage> GetChatMessagesByUsernames(string fromUsername, string toUsername)
        {
            using (var svc = new ServiceProxyHelper<IChatMessagesService>("ChatMessagesService"))
            {
                return svc.Proxy.GetChatMessagesByUsernames(fromUsername, toUsername);
            }
        }

        public ChatMessage AddChatMessage(ChatMessage chatMessage)
        {
            using (var svc = new ServiceProxyHelper<IChatMessagesService>("ChatMessagesService"))
            {
                return svc.Proxy.AddChatMessage(chatMessage);
            }
        }
    }
}