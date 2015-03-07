using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class ChatMessagesRestResource : IChatMessagesRestResource
    {
        public ChatMessagesList GetChatMessagesListByUserId(int userId, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<ChatMessagesList>(
                    svc.Get(Constants.BlogRestUrl, string.Format("user/{0}/chats", userId), authenticationToken));
                return result;
            }
        }

        public ChatMessagesList GetChatMessagesListByUsername(string username, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<ChatMessagesList>(
                    svc.Get(Constants.BlogRestUrl, string.Format("user/{0}/chats", username), authenticationToken));
                return result;
            }
        }

        public List<ChatMessage> GetChatMessagesByUserIds(int fromUserId, int toUserId, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<List<ChatMessage>>(
                    svc.Get(Constants.BlogRestUrl, string.Format("chat/{0}/{1}", fromUserId, toUserId), authenticationToken));
                return result;
            }
        }

        public List<ChatMessage> GetChatMessagesByUsernames(string fromUsername, string toUsername, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<List<ChatMessage>>(
                    svc.Get(Constants.BlogRestUrl, string.Format("chat/{0}/{1}", fromUsername, toUsername), authenticationToken));
                return result;
            }
        }

        public List<ChatMessage> GetMoreChatMessagesByUserIds(int fromUserId, int toUserId, string authenticationToken, int skip = 25)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<List<ChatMessage>>(
                    svc.Get(Constants.BlogRestUrl, string.Format("chat/{0}/{1}/more/{2}", fromUserId, toUserId, skip), authenticationToken));
                return result;
            }
        }

        public List<ChatMessage> GetMoreChatMessagesByUsernames(string fromUsername, string toUsername, string authenticationToken, int skip = 25)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<List<ChatMessage>>(
                   svc.Get(Constants.BlogRestUrl, string.Format("chat/{0}/{1}/more/{2}", fromUsername, toUsername, skip), authenticationToken));
                return result;
            }
        }

        public ChatMessage AddChatMessage(ChatMessage chatMessage, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<ChatMessage>(
                    svc.Post(Constants.BlogRestUrl, "album", chatMessage, authenticationToken));
                return result;
            }
        }
    }
}
