using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class UserRepository : GenericRepository<BlogDb, User>, IUserRepository
    {
        public IList<User> GetUsers(int threshold = 10, int skip = 10)
        {
            var query = Find(a => a.IsDeleted == false, null, null)
                .OrderByDescending(b => b.UserId)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public override User Edit(User user)
        {
            var db = Find(a => a.UserId == user.UserId, null, null).FirstOrDefault();
            if (db == null) throw new Exception("Record not found!");

            #region Joined Communities

            //var tempJoinedCommunityNames = db.JoinedCommunities.Select(a => a.Name).ToList();
            //var tempJoinedCommunities = tempJoinedCommunityNames.Select(c => 
            //    Context.Communities.FirstOrDefault(a => a.Name == c)).ToList();

            var tempJoinedCommunities = db.JoinedCommunities.ToList();
            foreach (var c in tempJoinedCommunities)
            {
                Context.Entry(c).State = GetCommunityState(c, user.JoinedCommunities);
            }

            var newCommunities = RepositoryHelper.GetNewCommunities(db.JoinedCommunities, user.JoinedCommunities, user.UserId);
            newCommunities.ForEach(a =>
            {
                Context.Communities.Attach(a);
                Context.Entry(a).State = EntityState.Added;
                db.JoinedCommunities.Add(a);
            });

            #endregion

            #region Created Communities

            //var tempCreatedCommunities = Context.Communities.Distinct()
            //    .Where(a => a.LeaderUserId == user.UserId).ToList();

            //var tempCreatedCommunities = db.CreatedCommunities.ToList();

            //foreach (var c in tempCreatedCommunities)
            //{
            //    Context.Entry(c).State = EntityState.Unchanged;
            //}
            
            #endregion

            db.FirstName = user.FirstName;
            db.LastName = user.LastName;
            db.EmailAddress = user.EmailAddress;
            db.BirthDate = user.BirthDate;
            db.UserName = user.UserName;
            db.BackgroundId = user.BackgroundId;
            db.PictureId = user.PictureId;
            db.IdentityId = user.IdentityId;

            Context.Entry(db).State = EntityState.Modified;
            Context.SaveChanges();

            return db;
        }

        private static EntityState GetCommunityState(Community community, IEnumerable<Community> communities)
        {
            if (communities == null) return EntityState.Unchanged;

            var names = communities.Select(a => a.Name.ToLower()).ToList();

            return names.Contains(community.Name.ToLower()) ?
                EntityState.Unchanged : EntityState.Deleted;
        }
    }
}
