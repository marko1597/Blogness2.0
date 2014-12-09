using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CommunityLogicTest
    {
        private Mock<ICommunityRepository> _communityRepository;
        private List<Community> _communities;
        private List<User> _members;

        [SetUp]
        public void TestInit()
        {
            #region User members

            _members = new List<User>
                    {
                        new User
                        {
                            UserId = 1,
                            UserName = "lorem"
                        },
                        new User
                        {
                            UserId = 2,
                            UserName = "ipsum"
                        },
                        new User
                        {
                            UserId = 3,
                            UserName = "dolor"
                        }
                    };

            #endregion

            #region Communities

            _communities = new List<Community>
                     {
                         new Community
                         {
                             Id = 1,
                             LeaderUserId = 1,
                             Leader = _members.FirstOrDefault(a => a.UserId == 1),
                             Members = _members.ToList()
                         },
                         new Community
                         {
                             Id = 2,
                             LeaderUserId = 2,
                             Leader = _members.FirstOrDefault(a => a.UserId == 2),
                             Members = _members.ToList()
                         },
                         new Community
                         {
                             Id = 3,
                             LeaderUserId = 3,
                             Leader = _members.FirstOrDefault(a => a.UserId == 3),
                             Members = _members.ToList()
                         },
                     };

            #endregion
        }

        [Test]
        public void ShouldGetCommunityId()
        {
            var communitySearch = new Community
                                  {
                                      Id = 1,
                                      LeaderUserId = 1,
                                      Leader = _members.FirstOrDefault(a => a.UserId == 1),
                                      Members = _members.ToList(),
                                      Posts = new List<Post> { new Post { PostId = 1 } }
                                  };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(communitySearch);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Get(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Posts);
            Assert.IsNull(result.Error);
        }

        [Test]
        public void ShouldReturnErrorWhenGetCommunityIdFoundNoRecords()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Get(It.IsAny<int>())).Returns((Community)null);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Get(1);

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("Cannot find community with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCommunityByIdFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Get(It.IsAny<int>())).Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.Get(1));
        }

        [Test]
        public void ShouldGetCommunityList()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetList(It.IsAny<int>())).Returns(_communities);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.GetList();

            Assert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof (Common.Contracts.Community));
            result.Select(a => a.Posts).ToList().ForEach(Assert.IsNull);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCommunityListFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetList(It.IsAny<int>())).Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.GetList());
        }

        [Test]
        public void ShouldGetMoreCommunityList()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetMore(It.IsAny<int>(), It.IsAny<int>())).Returns(_communities);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.GetList();

            Assert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Common.Contracts.Community));
            result.Select(a => a.Posts).ToList().ForEach(Assert.IsNull);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMoreCommunityListFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetMore(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.GetMore(5));
        }

        [Test]
        public void ShouldGetJoinedCommunityList()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetJoinedCommunitiesByUser(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(_communities);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.GetJoinedByUser(1);

            Assert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Common.Contracts.Community));
            result.Select(a => a.Posts).ToList().ForEach(Assert.IsNull);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetJoinedCommunityListFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetJoinedCommunitiesByUser(It.IsAny<int>(), It.IsAny<int>()))
                    .Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.GetJoinedByUser(1));
        }

        [Test]
        public void ShouldGetMoreJoinedCommunityList()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetMoreJoinedCommunitiesByUser(It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<int>())).Returns(_communities);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.GetMoreJoinedByUser(1, 5);

            Assert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Common.Contracts.Community));
            result.Select(a => a.Posts).ToList().ForEach(Assert.IsNull);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMoreJoinedCommunityListFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetMoreJoinedCommunitiesByUser(It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.GetMoreJoinedByUser(1, 5));
        }

        [Test]
        public void ShouldGetCreatedCommunityList()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetCreatedCommunitiesByUser(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(_communities);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.GetCreatedByUser(1);

            Assert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Common.Contracts.Community));
            result.Select(a => a.Posts).ToList().ForEach(Assert.IsNull);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCreatedCommunityListFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetCreatedCommunitiesByUser(It.IsAny<int>(), It.IsAny<int>()))
                    .Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.GetCreatedByUser(1));
        }

        [Test]
        public void ShouldGetMoreCreatedCommunityList()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetMoreCreatedCommunitiesByUser(It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<int>())).Returns(_communities);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.GetMoreCreatedByUser(1, 5);

            Assert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Common.Contracts.Community));
            result.Select(a => a.Posts).ToList().ForEach(Assert.IsNull);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMoreCreatedCommunityListFails()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.GetMoreCreatedCommunitiesByUser(It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.GetMoreCreatedByUser(1, 5));
        }

        [Test]
        public void ShouldAddCommunity()
        {
            var community = new Community { Id = 1, Name = "lorem", Description = "fudge brownies" };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Add(It.IsAny<Community>())).Returns(community);
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Returns(new List<Community>());

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Add(new Common.Contracts.Community
                                   {
                                       Name = "lorem",
                                       Description = "fudge brownies"
                                   });

            Assert.NotNull(result);
            Assert.IsNull(result.Error);
        }

        [Test]
        public void ShouldErrorWhenCommunityNameExistsOnAdd()
        {
            var search = _communities.Where(a => a.Id == 1).ToList();

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Returns(search);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Add(new Common.Contracts.Community
            {
                Name = "lorem",
                Description = "fudge brownies"
            });

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.ValidationError, result.Error.Id);
            Assert.AreEqual("Community name lorem is already in use.", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdatingCommunityFails()
        {
            var community = new Common.Contracts.Community
                            {
                                Name = "lorem",
                                Description = "fudge brownies"
                            };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Add(It.IsAny<Community>())).Throws(new Exception("Hooha!"));
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Returns(new List<Community>());

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.Add(community));
        }

        [Test]
        public void ShouldUpdateCommunity()
        {
            var community = new Community { Id = 1, Name = "lorem", Description = "fudge brownies" };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Edit(It.IsAny<Community>())).Returns(community);
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Returns(new List<Community>());

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Update(new Common.Contracts.Community
            {
                Id = 1,
                Name = "lorem",
                Description = "fudge brownies"
            });

            Assert.NotNull(result);
            Assert.IsNull(result.Error);
        }

        [Test]
        public void ShouldErrorWhenCommunityNameExistsOnUpdate()
        {
            var search = _communities.Where(a => a.Id == 1).ToList();

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Returns(search);

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Update(new Common.Contracts.Community
            {
                Name = "lorem",
                Description = "fudge brownies"
            });

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.ValidationError, result.Error.Id);
            Assert.AreEqual("Community name lorem is already in use.", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddingCommunityFails()
        {
            var community = new Common.Contracts.Community
            {
                Name = "lorem",
                Description = "fudge brownies"
            };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Add(It.IsAny<Community>())).Throws(new Exception("Hooha!"));
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Returns(new List<Community>());

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.Add(community));
        }

        [Test]
        public void ShouldThrowExceptionWhenCheckingCommunityNameFails()
        {
            var community = new Common.Contracts.Community
            {
                Name = "lorem",
                Description = "fudge brownies"
            };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), null, null))
                .Throws(new Exception("Hooha!"));

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.Add(community));
        }

        [Test]
        public void ShouldDeleteCommunity()
        {
            var community = new Community { Id = 1, Name = "lorem", Description = "fudge brownies" };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Delete(It.IsAny<Community>()));
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), false))
                .Returns(new List<Community> { community });

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenNoRecordFoundOnDelete()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), false))
                .Returns(new List<Community>());

            var logic = new CommunityLogic(_communityRepository.Object);
            var result = logic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeletingCommunityFailsOnGettingRecord()
        {
            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Delete(It.IsAny<Community>()));
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), false))
                .Throws(new Exception("Hooha!"));


            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.Delete(1));
        }

        [Test]
        public void ShouldThrowExceptionWhenDeletingCommunityFails()
        {
            var community = new Community { Id = 1, Name = "lorem", Description = "fudge brownies" };

            _communityRepository = new Mock<ICommunityRepository>();
            _communityRepository.Setup(a => a.Delete(It.IsAny<Community>())).Throws(new Exception("Hooha!"));
            _communityRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Community, bool>>>(), false))
                .Returns(new List<Community> { community });

            var logic = new CommunityLogic(_communityRepository.Object);

            Assert.Throws<BlogException>(() => logic.Delete(1));
        }
    }
}
