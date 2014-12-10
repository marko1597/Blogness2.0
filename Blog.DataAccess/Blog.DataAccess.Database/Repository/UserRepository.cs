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

        public IList<User> GetUsersByCommunity(int communityId, int threshold = 10, int skip = 10)
        {
            var query = Find(a => a.JoinedCommunities.Any(c => c.Id == communityId), false)
                .Distinct()
                .OrderByDescending(b => b.UserId)
                .Skip(skip)
                .Take(threshold)
                .ToList();

            foreach (var user in query)
            {
                user.Picture = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == user.PictureId);
                user.Background = Context.Media.AsNoTracking().FirstOrDefault(a => a.MediaId == user.BackgroundId);
            }

            return query;
        }

        public override User Edit(User user)
        {
            using (var context = new BlogDb())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var db = context.Users
                    .Include(a => a.CreatedCommunities)
                    .Include(a => a.JoinedCommunities)
                    .FirstOrDefault(a => a.UserId == user.UserId);

                if (db == null) throw new Exception("Record not found!");

                #region Joined Communities

                var tempJoinedCommunities = db.JoinedCommunities.ToList();
                foreach (var c in tempJoinedCommunities)
                {
                    context.Entry(c).State = GetCommunityState(c, user.JoinedCommunities);
                }

                var newCommunities = RepositoryHelper.GetNewCommunities(db.JoinedCommunities, user.JoinedCommunities, user.UserId);
                newCommunities.ForEach(a =>
                {
                    context.Communities.Attach(a);
                    context.Entry(a).State = EntityState.Added;
                    db.JoinedCommunities.Add(a);
                });

                #endregion

                #region Created Communities

                var tempCreatedCommunities = db.CreatedCommunities.ToList();

                foreach (var c in tempCreatedCommunities)
                {
                    context.Entry(c).State = EntityState.Unchanged;
                }

                #endregion

                db.FirstName = user.FirstName;
                db.LastName = user.LastName;
                db.EmailAddress = user.EmailAddress;
                db.BirthDate = user.BirthDate;
                db.UserName = user.UserName;
                db.BackgroundId = user.BackgroundId;
                db.PictureId = user.PictureId;
                db.IdentityId = user.IdentityId;

                context.Entry(db).State = EntityState.Modified;
                context.SaveChanges();

                return db; 
            }
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
