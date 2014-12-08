using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryHelper
    {
        public static List<Community> GetNewCommunities(IEnumerable<Community> dbCommunities, IEnumerable<Community> clientCommunities, int userId)
        {
            if (clientCommunities == null) return new List<Community>();

            var newCommunities = (from c in clientCommunities
                                  where dbCommunities.All(a => a.Id != c.Id)
                                  select c).ToList();

            foreach (var c in newCommunities)
            {
                c.CreatedDate = DateTime.Now;
                c.CreatedBy = userId;
                c.ModifiedDate = DateTime.Now;
                c.ModifiedBy = userId;
            }

            return newCommunities;
        }

        public static List<Tag> GetNewTags(IEnumerable<Tag> dbTags, IEnumerable<Tag> clientTags, int userId)
        {
            var dbTagNames = dbTags.Select(a => a.TagName.ToLower()).ToList();
            var newTags = (from t in clientTags
                where dbTagNames.All(a => a != t.TagName)
                select t).ToList();

            foreach (var t in newTags)
            {
                t.CreatedDate = DateTime.Now;
                t.CreatedBy = userId;
                t.ModifiedDate = DateTime.Now;
                t.ModifiedBy = userId;
            }

            return newTags;
        }

        public static List<PostContent> GetNewContents(IEnumerable<PostContent> dbContents, IEnumerable<PostContent> clientContents, int userId)
        {
            var newContents = (from c in clientContents
                where dbContents.All(a => a.MediaId != c.MediaId)
                select c).ToList();

            foreach (var pc in newContents)
            {
                pc.CreatedDate = DateTime.Now;
                pc.CreatedBy = userId;
                pc.ModifiedDate = DateTime.Now;
                pc.ModifiedBy = userId;
            }

            return newContents;
        }
    }
}
