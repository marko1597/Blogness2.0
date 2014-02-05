using System.Data.Entity;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.DataAccess.BlogService.Entities
{
    public class BlogDb : DbContext
    {
        public BlogDb() : base("name=BlogDb")
        {
            Database.SetInitializer(new BlogDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            base.OnModelCreating(mb);

            //Comments
            mb.Entity<Comment>().Ignore(a => a.User);
            mb.Entity<Comment>().Ignore(a => a.Comments);

            //Posts
            mb.Entity<Post>().Ignore(a => a.Tags);
            mb.Entity<Post>().Ignore(a => a.User);

            //Education
            mb.Entity<Education>().Ignore(a => a.EducationType);

            //Media
            mb.Entity<Media>().Ignore(a => a.MediaGroup);
            mb.Entity<Media>().Property(a => a.MediaContent).IsMaxLength();
            mb.Entity<Media>().Property(a => a.ThumbnailContent).IsMaxLength();
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<EducationType> EducationTypes { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<MediaGroup> MediaGroup { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<PostContent> PostContents { get; set; }
        
        public class BlogDbInitializer : DropCreateDatabaseAlways<BlogDb>
        {
            protected override void Seed(BlogDb dbContext)
            {
                base.Seed(dbContext);
                SeedLoader.InitializeSeedData();

                foreach (var u in SeedEntities.USERS)
                {
                    dbContext.Users.Add(u);
                }

                foreach (var a in SeedEntities.ADDRESS)
                {
                    dbContext.Address.Add(a);
                }

                foreach (var e in SeedEntities.EDUCATIONTYPE)
                {
                    dbContext.EducationTypes.Add(e);
                }

                foreach (var e in SeedEntities.EDUCATION)
                {
                    dbContext.Education.Add(e);
                }

                foreach (var h in SeedEntities.HOBBIES)
                {
                    dbContext.Hobbies.Add(h);
                }

                foreach (var t in SeedEntities.TAGS)
                {
                    dbContext.Tags.Add(t);
                }

                foreach (var p in SeedEntities.POSTS)
                {
                    dbContext.Posts.Add(p);
                }

                foreach (var pt in SeedEntities.POSTTAGS)
                {
                    dbContext.PostTags.Add(pt);
                }

                foreach (var pt in SeedEntities.POSTLIKES)
                {
                    dbContext.PostLikes.Add(pt);
                }

                foreach (var c in SeedEntities.COMMENTS)
                {
                    dbContext.Comments.Add(c);
                }

                foreach (var c in SeedEntities.COMMENTLIKES)
                {
                    dbContext.CommentLikes.Add(c);
                }

                foreach (var m in SeedEntities.MEDIAGROUP)
                {
                    dbContext.MediaGroup.Add(m);
                }

                foreach (var m in SeedEntities.MEDIA)
                {
                    dbContext.Media.Add(m);
                }

                foreach (var p in SeedEntities.POSTCONTENT)
                {
                    dbContext.PostContents.Add(p);
                }
            }
        }
    }
}