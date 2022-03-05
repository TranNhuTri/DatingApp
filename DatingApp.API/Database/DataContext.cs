using DatingApp.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLike> UserLikes {get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLike>()
                .HasKey(u => new {u.LikeUserId, u.SourceUserId});

            builder.Entity<UserLike>()
                .HasOne(u => u.LikeUser)
                .WithMany(u => u.LikedUsers)
                .HasForeignKey(u => u.LikeUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserLike>()
                .HasOne(u => u.SoureUser)
                .WithMany(u => u.SourceUsers)
                .HasForeignKey(u => u.SourceUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}