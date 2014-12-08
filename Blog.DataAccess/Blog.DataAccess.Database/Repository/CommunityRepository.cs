using System;
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
        public IList<Community> GetCommunitiesByUser(int userId, int threshold = 10)
        {
            var query = Find(a => a.Members.Any(u => u.UserId == userId), null, "Members,Posts")
                .OrderByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Community> GetMoreCommunitiesByUser(int userId, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.Members.Any(u => u.UserId == userId), null, "Members,Posts")
                .OrderByDescending(a => a.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public override Community Add(Community community)
        {
            // Set user members as unchanged state
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
