namespace HackerNews.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HackerNewsModel : DbContext
    {
        public HackerNewsModel()
            : base("name=HackerEntities")
        {
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.RelatedUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
