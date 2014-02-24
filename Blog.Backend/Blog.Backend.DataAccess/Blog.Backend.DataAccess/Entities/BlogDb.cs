using System.Data.Entity;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Entities
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

            //Users
            mb.Entity<User>()
                .HasMany<Education>(a => a.Education)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId);
            mb.Entity<User>()
                .HasMany<Hobby>(a => a.Hobbies)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId);
            mb.Entity<User>()
                .HasOptional<Address>(a => a.Address)
                .WithOptionalDependent();
            mb.Entity<User>()
                .HasMany<Comment>(a => a.Comments)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
            mb.Entity<User>()
                .HasMany<CommentLike>(a => a.CommentLikes)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
            mb.Entity<User>()
                .HasMany<Post>(a => a.Posts)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
            mb.Entity<User>()
                .HasMany<PostLike>(a => a.PostLikes)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
            mb.Entity<User>()
                .HasMany<MediaGroup>(a => a.MediaGroups)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
            mb.Entity<User>()
                .HasMany<Media>(a => a.Media)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            //Education
            mb.Entity<Education>()
                .HasRequired(a => a.EducationType)
                .WithMany(a => a.Education)
                .HasForeignKey(a => a.EducationTypeId);

            //MediaGroup
            mb.Entity<MediaGroup>()
                .HasMany<Media>(a => a.Media)
                .WithRequired(a => a.MediaGroup)
                .HasForeignKey(a => a.MediaGroupId);
            mb.Entity<PostContent>()
                .HasRequired(a => a.Media);

            //Comments
            mb.Entity<Comment>()
                .HasMany<CommentLike>(a => a.CommentLikes)
                .WithRequired(a => a.Comment)
                .HasForeignKey(a => a.CommentId);
            mb.Entity<Comment>()
                .HasMany<Comment>(a => a.Comments)
                .WithOptional(a => a.ParentComment)
                .HasForeignKey(a => a.ParentCommentId);
            
            //Posts
            mb.Entity<Post>()
                .HasMany<Comment>(a => a.Comments)
                .WithOptional(a => a.Post)
                .HasForeignKey(a => a.PostId);
            mb.Entity<Post>()
                .HasMany<PostLike>(a => a.PostLikes)
                .WithRequired(a => a.Post)
                .HasForeignKey(a => a.PostId);
            mb.Entity<Post>()
                .HasMany<PostLike>(a => a.PostLikes)
                .WithRequired(a => a.Post)
                .HasForeignKey(a => a.PostId);
            mb.Entity<Post>()
                .HasMany<PostContent>(a => a.PostContents)
                .WithRequired(a => a.Post)
                .HasForeignKey(a => a.PostId);
            mb.Entity<Post>()
                .HasMany<Tag>(a => a.Tags)
                .WithMany(a => a.Posts)
                .Map(x =>
                {
                    x.ToTable("PostTags");
                    x.MapLeftKey("PostId");
                    x.MapRightKey("TagId");
                });


        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
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
                //SeedLoader.InitializeSeedData();

                //foreach (var u in SeedEntities.Users)
                //{
                //    dbContext.Users.Add(u);
                //}

                //foreach (var a in SeedEntities.Address)
                //{
                //    dbContext.Address.Add(a);
                //}

                //foreach (var e in SeedEntities.Educationtype)
                //{
                //    dbContext.EducationTypes.Add(e);
                //}

                //foreach (var e in SeedEntities.Education)
                //{
                //    dbContext.Education.Add(e);
                //}

                //foreach (var h in SeedEntities.Hobbies)
                //{
                //    dbContext.Hobbies.Add(h);
                //}

                //foreach (var t in SeedEntities.Tags)
                //{
                //    dbContext.Tags.Add(t);
                //}

                //foreach (var p in SeedEntities.Posts)
                //{
                //    dbContext.Posts.Add(p);
                //}

                //foreach (var pt in SeedEntities.PostTags)
                //{
                //    dbContext.PostTags.Add(pt);
                //}

                //foreach (var pt in SeedEntities.PostLikes)
                //{
                //    dbContext.PostLikes.Add(pt);
                //}

                //foreach (var c in SeedEntities.Comments)
                //{
                //    dbContext.Comments.Add(c);
                //}

                //foreach (var c in SeedEntities.CommentLikes)
                //{
                //    dbContext.CommentLikes.Add(c);
                //}

                //foreach (var m in SeedEntities.MediaGroups)
                //{
                //    dbContext.MediaGroup.Add(m);
                //}

                //foreach (var m in SeedEntities.Media)
                //{
                //    dbContext.Media.Add(m);
                //}

                //foreach (var p in SeedEntities.PostContents)
                //{
                //    dbContext.PostContents.Add(p);
                //}
            }
        }
    }
}