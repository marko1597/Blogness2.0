﻿using System.Linq;
using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class PostMapper
    {
        public static Post ToDto(Db.Post post)
        {
            if (post == null) return null;

            var postLikes = post.PostLikes != null
                ? post.PostLikes.Select(PostLikeMapper.ToDto).ToList()
                : null;
            var contents = post.PostContents != null
                ? post.PostContents.Select(PostContentMapper.ToDto).ToList()
                : null;
            var comments = post.Comments != null
                ? post.Comments.Select(CommentMapper.ToDto).ToList()
                : null;
            var tags = post.Tags != null
                ? post.Tags.Select(TagMapper.ToDto).ToList()
                : null;
            var viewCounts = post.ViewCounts != null
                ? post.ViewCounts.Select(ViewCountMapper.ToDto).ToList()
                : null;
            var communities = post.Communities != null
                ? post.Communities.Select(CommunityMapper.ToDto).ToList()
                : null;

            return new Post
                   {
                       Id = post.PostId,
                       PostTitle = post.PostTitle,
                       PostMessage = post.PostMessage,
                       PostLikes = postLikes,
                       PostContents = contents,
                       Comments = comments,
                       ViewCounts = viewCounts,
                       Communities = communities,
                       User = post.User != null ? UserMapper.ToDto(post.User) : null,
                       Tags = tags,
                       CreatedBy = post.CreatedBy,
                       CreatedDate = post.CreatedDate,
                       ModifiedBy = post.ModifiedBy,
                       ModifiedDate = post.ModifiedDate
                   };
        }

        public static Db.Post ToEntity(Post post)
        {
            if (post == null) return null;

            var postLikes = post.PostLikes != null
                ? post.PostLikes.Select(PostLikeMapper.ToEntity).ToList()
                : null;
            var contents = post.PostContents != null
                ? post.PostContents.Select(PostContentMapper.ToEntity).ToList()
                : null;
            var comments = post.Comments != null
                ? post.Comments.Select(CommentMapper.ToEntity).ToList()
                : null;
            var tags = post.Tags != null
                ? post.Tags.Select(TagMapper.ToEntity).ToList()
                : null;
            var viewCounts = post.ViewCounts != null
                ? post.ViewCounts.Select(ViewCountMapper.ToEntity).ToList()
                : null;
            var communities = post.Communities != null
                ? post.Communities.Select(CommunityMapper.ToEntity).ToList()
                : null;

            return new Db.Post
                   {
                       PostId = post.Id,
                       PostTitle = post.PostTitle,
                       PostMessage = post.PostMessage,
                       PostLikes = postLikes,
                       PostContents = contents,
                       Comments = comments,
                       ViewCounts = viewCounts,
                       Communities = communities,
                       User = null,
                       UserId = post.User != null ? post.User.Id : 0,
                       Tags = tags,
                       CreatedBy = post.CreatedBy,
                       CreatedDate = post.CreatedDate,
                       ModifiedBy = post.ModifiedBy,
                       ModifiedDate = post.ModifiedDate
                   };
        }
    }
}
