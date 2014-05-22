using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class PostMapper
    {
        public static Post ToDto(DataAccess.Database.Entities.Objects.Post post)
        {
            if (post != null)
            {
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

                return new Post
                {
                    PostId = post.PostId,
                    PostTitle = post.PostTitle,
                    PostMessage = post.PostMessage,
                    PostLikes = postLikes,
                    PostContents = contents,
                    Comments = comments,
                    User = post.User != null ? UserMapper.ToDto(post.User) : null,
                    Tags = tags,
                    CreatedBy = post.CreatedBy,
                    CreatedDate = post.CreatedDate,
                    ModifiedBy = post.ModifiedBy,
                    ModifiedDate = post.ModifiedDate
                };
            }
            return new Post();
        }

        public static DataAccess.Database.Entities.Objects.Post ToEntity(Post post)
        {
            if (post != null)
            {
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

                return new DataAccess.Database.Entities.Objects.Post
                {
                    PostId = post.PostId,
                    PostTitle = post.PostTitle,
                    PostMessage = post.PostMessage,
                    PostLikes = postLikes,
                    PostContents = contents,
                    Comments = comments,
                    User = null,
                    UserId = post.User != null ? post.User.UserId : 0,
                    Tags = tags,
                    CreatedBy = post.CreatedBy,
                    CreatedDate = post.CreatedDate,
                    ModifiedBy = post.ModifiedBy,
                    ModifiedDate = post.ModifiedDate
                };
            }
            return new DataAccess.Database.Entities.Objects.Post();
        }
    }
}
