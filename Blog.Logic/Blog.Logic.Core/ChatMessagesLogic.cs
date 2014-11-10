using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class ChatMessagesLogic : IChatMessagesLogic
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUserRepository _userRepository;

        public ChatMessagesLogic(IChatMessageRepository chatMessageRepository, IUserRepository userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _userRepository = userRepository;
        }

        public ChatMessagesList GetChatMessagesListByUser(int userId)
        {
            try
            {
                return GetChatMessagesList(userId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public ChatMessagesList GetChatMessagesListByUser(string username)
        {
            try
            {
                var user = GetUserByUsername(username);
                if (user == null)
                    return new ChatMessagesList()
                    .GenerateError<ChatMessagesList>((int)Constants.Error.RecordNotFound,
                        string.Format("No user found with username {0}", username));

                return GetChatMessagesList(user.Id);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<ChatMessage> GetChatMessagesByUser(int fromUserId, int toUserId)
        {
            try
            {
                return GetChatMessages(fromUserId, toUserId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<ChatMessage> GetChatMessagesByUser(string fromUsername, string toUsername)
        {
            try
            {
                var fromUser = GetUserByUsername(fromUsername);
                if (fromUser == null) 
                    throw new Exception(string.Format("No user found with username {0}", fromUsername));

                var toUser = GetUserByUsername(toUsername);
                if (toUser == null)
                    throw new Exception(string.Format("No user found with username {0}", toUsername));

                return GetChatMessages(fromUser.Id, toUser.Id);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<ChatMessage> GetMoreChatMessagesByUser(int fromUserId, int toUserId, int skip = 25)
        {
            try
            {
                return GetMoreChatMessages(fromUserId, toUserId, skip);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<ChatMessage> GetMoreChatMessagesByUser(string fromUsername, string toUsername, int skip = 25)
        {
            try
            {
                var fromUser = GetUserByUsername(fromUsername);
                if (fromUser == null)
                    throw new Exception(string.Format("No user found with username {0}", fromUsername));

                var toUser = GetUserByUsername(toUsername);
                if (toUser == null)
                    throw new Exception(string.Format("No user found with username {0}", toUsername));

                return GetMoreChatMessages(fromUser.Id, toUser.Id, skip);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public ChatMessage Add(ChatMessage chatMessage)
        {
            try
            {
                var dbChatMessage = ChatMessageMapper.ToEntity(chatMessage);
                dbChatMessage.FromUser = null;
                dbChatMessage.ToUser = null;
                dbChatMessage.CreatedBy = dbChatMessage.FromUserId;
                dbChatMessage.ModifiedBy = dbChatMessage.FromUserId;

                var addResult = _chatMessageRepository.Add(dbChatMessage);
                var newChatMessage = _chatMessageRepository.Find(a => a.ChatMessageId == addResult.ChatMessageId, null, "FromUser,ToUser").FirstOrDefault();

                if (newChatMessage == null || newChatMessage.FromUser == null || newChatMessage.ToUser == null)
                {
                    throw new Exception("Successfully created message but failed to get users related to the message.");
                }

                return ChatMessageMapper.ToDto(newChatMessage);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private List<ChatMessage> GetMoreChatMessages(int fromUserId, int toUserId, int skip = 25)
        {
            var dbChatMessages = _chatMessageRepository.GetMoreChatMessages(fromUserId, toUserId, skip);
            var chatMessages = new List<ChatMessage>();
            dbChatMessages.ForEach(a => chatMessages.Add(ChatMessageMapper.ToDto(a)));

            return chatMessages;
        }

        private List<ChatMessage> GetChatMessages(int fromUserId, int toUserId)
        {
            var dbChatMessages = _chatMessageRepository.GetChatMessages(fromUserId, toUserId);
            var chatMessages = new List<ChatMessage>();
            dbChatMessages.ForEach(a => chatMessages.Add(ChatMessageMapper.ToDto(a)));
            
            return chatMessages;
        }

        private ChatMessagesList GetChatMessagesList(int userId)
        {
            var userChatMessages = _chatMessageRepository.GetUserChatMessages(userId);
            if (userChatMessages == null)
                return new ChatMessagesList()
                    .GenerateError<ChatMessagesList>((int)Constants.Error.RecordNotFound,
                        string.Format("No chat messages found for user with Id {0}", userId));

            var chatMessagesList = new ChatMessagesList
            {
                ChatMessageListItems = new List<ChatMessageListItem>()
            };

            foreach (var chatMessage in userChatMessages)
            {
                var chatMessageItem = new ChatMessageListItem
                {
                    User = UserMapper.ToDto(chatMessage.User),
                    LastChatMessage = ChatMessageMapper.ToDto(chatMessage.LastChatMessage)
                };

                chatMessagesList.ChatMessageListItems.Add(chatMessageItem);
            }

            return chatMessagesList;
        }

        private User GetUserByUsername(string username)
        {
            var user = _userRepository.Find(a => a.UserName == username, false).FirstOrDefault();
            return UserMapper.ToDto(user);
        }
    }
}
