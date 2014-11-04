using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ChatMessagesLogicTest
    {
        private Mock<IChatMessageRepository> _chatMessageRepository;

        private ChatMessagesLogic _chatMessagesLogic;

        private List<ChatMessage> _chatMessages;

        private List<UserChatMessage> _userChatMessages;

        [SetUp]
        public void TestInit()
        {
            #region Chat Messages

            _chatMessages = new List<ChatMessage>
                     {
                         new ChatMessage
                         {
                             ChatMessageId = 1,
                             Text = "Lorem ipsum dolor sit amet",
                             FromUserId = 1,
                             FromUser = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    },
                             ToUserId = 2,
                             ToUser = new User
                                    {
                                        UserId = 2,
                                        UserName = "Drums"
                                    }
                         },
                         new ChatMessage
                         {
                             ChatMessageId = 2,
                             Text = "Lorem ipsum dolor sit amet",
                             FromUserId = 1,
                             FromUser = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    },
                             ToUserId = 3,
                             ToUser = new User
                                    {
                                        UserId = 3,
                                        UserName = "Penny"
                                    }
                         },
                         new ChatMessage
                         {
                             ChatMessageId = 3,
                             Text = "Lorem ipsum dolor sit amet",
                             FromUserId = 2,
                             FromUser = new User
                                    {
                                        UserId = 2,
                                        UserName = "Drums"
                                    },
                             ToUserId = 1,
                             ToUser = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    }
                         },
                         new ChatMessage
                         {
                             ChatMessageId = 4,
                             Text = "Lorem ipsum dolor sit amet",
                             FromUserId = 2,
                             FromUser = new User
                                    {
                                        UserId = 2,
                                        UserName = "Drums"
                                    },
                             ToUserId = 3,
                             ToUser = new User
                                    {
                                        UserId = 3,
                                        UserName = "Penny"
                                    }
                         },
                         new ChatMessage
                         {
                             ChatMessageId = 5,
                             Text = "Lorem ipsum dolor sit amet",
                             FromUserId = 3,
                             FromUser = new User
                                    {
                                        UserId = 3,
                                        UserName = "Penny"
                                    },
                             ToUserId = 1,
                             ToUser = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    }
                         },
                         new ChatMessage
                         {
                             ChatMessageId = 6,
                             Text = "Lorem ipsum dolor sit amet",
                             FromUserId = 3,
                             FromUser = new User
                                    {
                                        UserId = 3,
                                        UserName = "Penny"
                                    },
                             ToUserId = 2,
                             ToUser = new User
                                    {
                                        UserId = 2,
                                        UserName = "Drums"
                                    }
                         }
                     };

            #endregion

            #region User Chat Messages

            _userChatMessages = new List<UserChatMessage>
                     {
                         new UserChatMessage
                         {
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Drums"
                                    },
                             ChatMessages = new List<ChatMessage>
                                    {
                                        new ChatMessage
                                        {
                                            ChatMessageId = 1,
                                            Text = "Lorem ipsum dolor sit amet",
                                            FromUserId = 2,
                                            FromUser = new User
                                                    {
                                                        UserId = 2,
                                                        UserName = "Drums"
                                                    },
                                            ToUserId = 1,
                                            ToUser = new User
                                                    {
                                                        UserId = 1,
                                                        UserName = "FooBar"
                                                    }
                                         }   
                                    }
                         },
                         new UserChatMessage
                         {
                             User = new User
                                    {
                                        UserId = 3,
                                        UserName = "Penny"
                                    },
                             ChatMessages = new List<ChatMessage>
                                    {
                                        new ChatMessage
                                        {
                                            ChatMessageId = 2,
                                            Text = "Lorem ipsum dolor sit amet",
                                            FromUserId = 3,
                                            FromUser = new User
                                                    {
                                                        UserId = 3,
                                                        UserName = "Penny"
                                                    },
                                            ToUserId = 1,
                                            ToUser = new User
                                                    {
                                                        UserId = 1,
                                                        UserName = "FooBar"
                                                    }
                                         }   
                                    }
                         }
                     };

            #endregion
        }

        [TestMethod]
        public void ShouldGetChatListMessagesById()
        {
        }
    }
}
