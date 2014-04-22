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
                .HasMany<Album>(a => a.Albums)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            //Education
            mb.Entity<Education>()
                .HasRequired(a => a.EducationType)
                .WithMany(a => a.Education)
                .HasForeignKey(a => a.EducationTypeId);

            //Album
            mb.Entity<Album>()
                .HasMany<Media>(a => a.Media)
                .WithRequired(a => a.Album)
                .HasForeignKey(a => a.AlbumId);
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
        public DbSet<Album> Albums { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<PostContent> PostContents { get; set; }

        public class BlogDbInitializer : CreateDatabaseIfNotExists<BlogDb>
        {
        }
    }
}