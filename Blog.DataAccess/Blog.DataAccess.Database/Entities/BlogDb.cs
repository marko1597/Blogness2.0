﻿using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Entities
{
    [ExcludeFromCodeCoverage]
    public class BlogDb : DbContext
    {
        public BlogDb() : base("name=BlogDb")
        {
            System.Data.Entity.Database.SetInitializer(new BlogDbInitializer());
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
            mb.Entity<User>()
                .HasOptional<Media>(a => a.Picture)
                .WithOptionalPrincipal();
            mb.Entity<User>()
                .HasOptional<Media>(a => a.Background)
                .WithOptionalPrincipal();
            mb.Entity<User>()
                .HasOptional<Address>(a => a.Address)
                .WithOptionalPrincipal()
                .WillCascadeOnDelete(false);
            
            // Chat Message
            mb.Entity<ChatMessage>()
                .HasRequired(a => a.FromUser)
                .WithMany(a => a.SentChatMessages)
                .HasForeignKey(a => a.FromUserId)
                .WillCascadeOnDelete(false);
            mb.Entity<ChatMessage>()
                .HasRequired(a => a.ToUser)
                .WithMany(a => a.ReceivedChatMessages)
                .HasForeignKey(a => a.ToUserId)
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

            // Communities
            mb.Entity<Community>()
                .HasMany<User>(a => a.Members)
                .WithMany(a => a.JoinedCommunities)
                .Map(x =>
                {
                    x.ToTable("UserCommunities");
                    x.MapLeftKey("CommunityId");
                    x.MapRightKey("UserId");
                });
            mb.Entity<Community>()
                .HasRequired<User>(a => a.Leader)
                .WithMany(a => a.CreatedCommunities)
                .HasForeignKey(a => a.LeaderUserId)
                .WillCascadeOnDelete(false);
            mb.Entity<Community>()
                .HasOptional<Media>(a => a.Emblem)
               .WithOptionalPrincipal()
                .WillCascadeOnDelete(false);

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
                .HasMany<ViewCount>(a => a.ViewCounts)
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
            mb.Entity<Post>()
                .HasMany<Community>(a => a.Communities)
                .WithMany(a => a.Posts)
                .Map(x =>
                {
                    x.ToTable("CommunityPosts");
                    x.MapLeftKey("PostId");
                    x.MapRightKey("CommunityId");
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
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<ViewCount> ViewCounts { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<PostContent> PostContents { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Community> Communities { get; set; }

        public class BlogDbInitializer : CreateDatabaseIfNotExists<BlogDb>
        {
        }
    }
}