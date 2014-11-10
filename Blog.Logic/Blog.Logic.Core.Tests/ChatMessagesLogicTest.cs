using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ChatMessagesLogicTest
    {
        private Mock<IChatMessageRepository> _chatMessageRepository;

        private Mock<IUserRepository> _userRepository;

        private ChatMessagesLogic _chatMessagesLogic;

        private List<ChatMessage> _chatMessages;

        private List<UserChatMessage> _userChatMessages;

        private List<User> _user;

        [SetUp]
        public void TestInit()
        {
            #region User

            _user = new List<User> { new User { UserId = 1, UserName = "FooBar" } };

            #endregion

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
                             LastChatMessage = new ChatMessage
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
                         },
                         new UserChatMessage
                         {
                             User = new User
                                    {
                                        UserId = 3,
                                        UserName = "Penny"
                                    },
                             LastChatMessage = new ChatMessage
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
                     };

            #endregion
        }

        [Test]
        public void ShouldGetChatListMessagesById()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Returns(_userChatMessages);

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesListByUser(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ChatMessagesList), result);
            Assert.AreEqual(result.ChatMessageListItems.Count, 2);
        }

        [Test]
        public void ShouldGetErrorWhenGettingChatListMessagesByIdReturnsNull()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Returns((List<UserChatMessage>)null);

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesListByUser(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ChatMessagesList), result);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No chat messages found for user with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingChatListMessagesByIdFails()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesListByUser(1));
        }

        [Test]
        public void ShouldGetChatListMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Returns(_userChatMessages);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(_user);

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesListByUser("adhesive");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ChatMessagesList), result);
            Assert.AreEqual(result.ChatMessageListItems.Count, 2);
        }

        [Test]
        public void ShouldGetErrorWhenGettingChatListMessagesByNameCannotFindUser()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Returns((List<UserChatMessage>)null);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(new List<User>());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesListByUser("adhesive");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ChatMessagesList), result);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
        }

        [Test]
        public void ShouldGetErrorWhenGettingChatListMessagesByNameReturnsNull()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Returns((List<UserChatMessage>)null);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(_user);

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesListByUser("adhesive");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ChatMessagesList), result);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No chat messages found for user with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingChatListMessagesByNameFailsOnGettingUser()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Throws(new Exception());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesListByUser("adhesive"));
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingChatListMessagesByNameFailsOnGettingChatList()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetUserChatMessages(It.IsAny<int>())).Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(_user);

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesListByUser("adhesive"));
        }

        [Test]
        public void ShouldGetMoreChatMessagesById()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_chatMessages);

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetMoreChatMessagesByUser(1, 2, 15);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.ChatMessage>), result);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGettingMoreChatMessagesByIdIsNull()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetMoreChatMessagesByUser(1, 2, 15);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.ChatMessage>), result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingMoreChatMessagesByIdFails()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetMoreChatMessagesByUser(1, 2, 15));
        }

        [Test]
        public void ShouldGetMoreChatMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_chatMessages);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(_user);

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetMoreChatMessagesByUser("FooBar", "Drums", 15);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.ChatMessage>), result);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingFromUserIsNullOnMoreChatMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(new List<User>());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = Assert.Throws<BlogException>(() => 
                _chatMessagesLogic.GetMoreChatMessagesByUser("FooBar", "Drums", 15));
            Assert.AreEqual("No user found with username FooBar", result.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingToUserIsNullOnMoreChatMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.SetupSequence(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(_user)
                .Returns(new List<User>());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = Assert.Throws<BlogException>(() => _chatMessagesLogic.GetMoreChatMessagesByUser("FooBar", "Drums", 15));
            Assert.AreEqual("No user found with username Drums", result.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingUserOnGetMoreChatMessagesByNameFails()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetMoreChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Throws(new Exception());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetMoreChatMessagesByUser("FooBar", "Drums", 15));
        }

        [Test]
        public void ShouldGetChatMessagesById()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_chatMessages);

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesByUser(1, 2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.ChatMessage>), result);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGettingChatMessagesByIdIsNull()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesByUser(1, 2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.ChatMessage>), result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingChatMessagesByIdFails()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesByUser(1, 2));
        }

        [Test]
        public void ShouldGetChatMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_chatMessages);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(_user);

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.GetChatMessagesByUser("FooBar", "Drums");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Common.Contracts.ChatMessage>), result);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingFromUserIsNullOnChatMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(new List<User>());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesByUser("FooBar", "Drums"));
            Assert.AreEqual("No user found with username FooBar", result.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingToUserIsNullOnChatMessagesByName()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.SetupSequence(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(_user)
                .Returns(new List<User>());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesByUser("FooBar", "Drums"));
            Assert.AreEqual("No user found with username Drums", result.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGettingUserOnGetChatMessagesByNameFails()
        {
            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.GetChatMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Throws(new Exception());

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.GetChatMessagesByUser("FooBar", "Drums"));
        }
        
        [Test]
        public void ShouldAddChatMessage()
        {
            var dbResult = _chatMessages.FirstOrDefault();
            var chatMessageParam = new Common.Contracts.ChatMessage
            {
                Id = 1,
                Text = "Lorem ipsum dolor sit amet",
                FromUser = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                },
                ToUser = new Common.Contracts.User
                {
                    Id = 2,
                    UserName = "Drums"
                }
            };

            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.Add(It.IsAny<ChatMessage>())).Returns(dbResult);
            _chatMessageRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ChatMessage, bool>>>(), null, It.IsAny<string>())).Returns(_chatMessages);

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var result = _chatMessagesLogic.Add(chatMessageParam);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.FromUser);
            Assert.IsNotNull(result.ToUser);
            Assert.IsInstanceOf(typeof(Common.Contracts.ChatMessage), result);
        }

        [Test]
        public void ShouldThrowExceptionWhenSuccessfullyAddedChatButChatIsNull()
        {
            var dbResult = _chatMessages.FirstOrDefault();
            var chatMessageParam = new Common.Contracts.ChatMessage
            {
                Id = 1,
                Text = "Lorem ipsum dolor sit amet",
                FromUser = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                },
                ToUser = new Common.Contracts.User
                {
                    Id = 2,
                    UserName = "Drums"
                }
            };

            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.Add(It.IsAny<ChatMessage>())).Returns(dbResult);
            _chatMessageRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ChatMessage, bool>>>(), null, It.IsAny<string>())).Returns(new List<ChatMessage>());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var exception = Assert.Throws<BlogException>(() => _chatMessagesLogic.Add(chatMessageParam));

            Assert.AreEqual("Successfully created message but failed to get users related to the message.", exception.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenSuccessfullyAddedChatButFromUserIsNull()
        {
            var addDbResult = _chatMessages.FirstOrDefault();
            var getDbResult = _chatMessages.FirstOrDefault() ?? new ChatMessage();
            getDbResult.FromUser = null;

            var chatMessageParam = new Common.Contracts.ChatMessage
            {
                Id = 1,
                Text = "Lorem ipsum dolor sit amet",
                FromUser = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                },
                ToUser = new Common.Contracts.User
                {
                    Id = 2,
                    UserName = "Drums"
                }
            };

            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.Add(It.IsAny<ChatMessage>())).Returns(addDbResult);
            _chatMessageRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ChatMessage, bool>>>(), null, It.IsAny<string>())).Returns(new List<ChatMessage> { getDbResult });

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var exception = Assert.Throws<BlogException>(() => _chatMessagesLogic.Add(chatMessageParam));

            Assert.AreEqual("Successfully created message but failed to get users related to the message.", exception.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenSuccessfullyAddedChatButToUserIsNull()
        {
            var addDbResult = _chatMessages.FirstOrDefault();
            var getDbResult = _chatMessages.FirstOrDefault() ?? new ChatMessage();
            getDbResult.ToUser = null;

            var chatMessageParam = new Common.Contracts.ChatMessage
            {
                Id = 1,
                Text = "Lorem ipsum dolor sit amet",
                FromUser = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                },
                ToUser = new Common.Contracts.User
                {
                    Id = 2,
                    UserName = "Drums"
                }
            };

            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.Add(It.IsAny<ChatMessage>())).Returns(addDbResult);
            _chatMessageRepository.Setup(a => a.Find(It.IsAny<Expression<Func<ChatMessage, bool>>>(), null, It.IsAny<string>())).Returns(new List<ChatMessage> { getDbResult });

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            var exception = Assert.Throws<BlogException>(() => _chatMessagesLogic.Add(chatMessageParam));

            Assert.AreEqual("Successfully created message but failed to get users related to the message.", exception.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddingChatMessageFails()
        {
            var chatMessageParam = new Common.Contracts.ChatMessage
            {
                Id = 1,
                Text = "Lorem ipsum dolor sit amet",
                FromUser = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                },
                ToUser = new Common.Contracts.User
                {
                    Id = 2,
                    UserName = "Drums"
                }
            };

            _chatMessageRepository = new Mock<IChatMessageRepository>();
            _chatMessageRepository.Setup(a => a.Add(It.IsAny<ChatMessage>())).Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();

            _chatMessagesLogic = new ChatMessagesLogic(_chatMessageRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _chatMessagesLogic.Add(chatMessageParam));
        }
    }
}
