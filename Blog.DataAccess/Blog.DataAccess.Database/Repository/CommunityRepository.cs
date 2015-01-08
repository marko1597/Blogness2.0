﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class CommunityRepository : GenericRepository<BlogDb, Community>, ICommunityRepository
    {
        public Community Get(int id)
        {
            var db = Find(a => a.LeaderUserId == id, null, "Members,Leader,Posts").FirstOrDefault();
            if (db == null) return null;

            var members = db.Members
                .OrderByDescending(b => b.UserId)
                .Take(10)
                .ToList();

            db.Members = members;

            db.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == db.Leader.PictureId);
            db.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == db.Leader.BackgroundId);
            db.Leader.Posts = null;
            db.Leader.PostLikes = null;
            db.Leader.Albums = null;
            db.Leader.Comments = null;
            db.Leader.CommentLikes = null;
            db.Leader.CreatedCommunities = null;
            db.Leader.JoinedCommunities = null;
            db.Leader.SentChatMessages = null;
            db.Leader.ReceivedChatMessages = null;
            db.Leader.Education = null;
            db.Leader.Hobbies = null;

            foreach (var post in db.Posts)
            {
                post.Communities = null;
                post.User.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == post.User.PictureId);
                post.User.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == post.User.BackgroundId);
            }

            foreach (var member in db.Members)
            {
                member.Posts = null;
                member.PostLikes = null;
                member.Albums = null;
                member.Comments = null;
                member.CommentLikes = null;
                member.CreatedCommunities = null;
                member.JoinedCommunities = null;
                member.SentChatMessages = null;
                member.ReceivedChatMessages = null;
                member.Education = null;
                member.Hobbies = null;
                member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
            }

            return db;
        }

        public IList<Community> GetList(int threshold = 10)
        {
            Context.Configuration.ProxyCreationEnabled = false;

            var query = Context.Communities
                .Include(a => a.Members)
                .OrderByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();

            foreach (var community in query)
            {
                community.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.PictureId);
                community.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.BackgroundId);
                foreach (var member in community.Members)
                {
                    member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                    member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
                }
            }

            return query;
        }

        public IList<Community> GetMore(int threshold = 5, int skip = 10)
        {
            Context.Configuration.ProxyCreationEnabled = false;

            var query = Context.Communities
                .Include(a => a.Members)
                .OrderByDescending(a => a.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();

            foreach (var community in query)
            {
                community.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.PictureId);
                community.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.BackgroundId);
                foreach (var member in community.Members)
                {
                    member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                    member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
                }
            }

            return query;
        }

        public IList<Community> GetJoinedCommunitiesByUser(int userId, int threshold = 10)
        {
            var query = Find(a => a.Members.Any(u => u.UserId == userId), null, "Members")
                .OrderByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();

            foreach (var community in query)
            {
                community.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.PictureId);
                community.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.BackgroundId);
                foreach (var member in community.Members)
                {
                    member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                    member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
                }
            }
            
            return query;
        }

        public IList<Community> GetMoreJoinedCommunitiesByUser(int userId, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.Members.Any(u => u.UserId == userId), null, "Members")
                .OrderByDescending(a => a.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();

            foreach (var community in query)
            {
                community.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.PictureId);
                community.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.BackgroundId);
                foreach (var member in community.Members)
                {
                    member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                    member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
                }
            }

            return query;
        }

        public IList<Community> GetCreatedCommunitiesByUser(int userId, int threshold = 10)
        {
            var query = Find(a => a.LeaderUserId == userId, null, "Members")
                .OrderByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();

            foreach (var community in query)
            {
                community.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.PictureId);
                community.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.BackgroundId);
                foreach (var member in community.Members)
                {
                    member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                    member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
                }
            }

            return query;
        }

        public IList<Community> GetMoreCreatedCommunitiesByUser(int userId, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.LeaderUserId == userId, null, "Members")
                .OrderByDescending(a => a.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();

            foreach (var community in query)
            {
                community.Leader.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.PictureId);
                community.Leader.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == community.Leader.BackgroundId);
                foreach (var member in community.Members)
                {
                    member.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.PictureId);
                    member.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == member.BackgroundId);
                }
            }

            return query;
        }

        public override Community Add(Community community)
        {
            if (community.Members != null)
            {
                var members = community.Members;
                community.Members = new List<User>();

                foreach (var m in members)
                {
                    Context.Users.Attach(m);
                    Context.Entry(m).State = EntityState.Unchanged;
                    community.Members.Add(m);
                }
            }

            community.CreatedDate = DateTime.Now;
            community.CreatedBy = community.LeaderUserId;
            community.ModifiedDate = DateTime.Now;
            community.ModifiedBy = community.LeaderUserId;

            Context.Entry(community).State = EntityState.Added;
            Context.SaveChanges();

            return community;
        }

        public override Community Edit(Community community)
        {
            var db = Context.Communities
                .Include(a => a.Members)
                .Include(a => a.Posts)
                .FirstOrDefault(a => a.Id == community.Id);

            if (db == null) throw new Exception("Record not found!");

            db.Name = community.Name;
            db.Description = community.Description;

            var tempMembers = db.Members.ToList();
            foreach (var t in tempMembers)
            {
                Context.Entry(t).State = GetMemberState(t, community.Members);
            }

            var newMembers = GetNewMembers(db.Members, community.Members);
            newMembers.ForEach(a =>
                            {
                                Context.Users.Attach(a);
                                Context.Entry(a).State = EntityState.Added;
                                db.Members.Add(a);
                            });

            var tempPosts = db.Posts.ToList();
            foreach (var p in tempPosts)
            {
                var tPost = community.Posts.FirstOrDefault(a => a.PostId == p.PostId);
                Context.Entry(p).State = tPost != null ? EntityState.Modified : EntityState.Deleted;
            }

            db.ModifiedDate = DateTime.Now;
            db.ModifiedBy = community.LeaderUserId;

            Context.Entry(db).State = EntityState.Modified;
            Context.SaveChanges();

            return community;
        }

        public int GetMemberCountByCommunity(int id)
        {
            var query = Find(a => a.Id == id, null, "Members")
                .Select(a => a.Members)
                .FirstOrDefault()
                .Count;

            return query;
        }

        #region Private methods

        private static EntityState GetMemberState(User user, IEnumerable<User> users)
        {
            var userNames = users.Select(a => a.UserName.ToLower()).ToList();

            return userNames.Contains(user.UserName.ToLower()) ?
                EntityState.Unchanged : EntityState.Deleted;
        }

        private static List<User> GetNewMembers(IEnumerable<User> dbMembers, IEnumerable<User> clientMembers)
        {
            var dbUserNames = dbMembers.Select(a => a.UserName.ToLower()).ToList();
            var newMembers = (from t in clientMembers
                              where dbUserNames.All(a => a != t.UserName)
                              select t).ToList();

            return newMembers;
        }

        #endregion
    }
}