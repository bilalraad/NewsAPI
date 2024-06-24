using Microsoft.EntityFrameworkCore;
using NewsAPI.Entities;

namespace NewsAPI;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public DbSet<NewsLike> NewsLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NewsLike>()
            .HasKey(nl => new { nl.SourceUserId, nl.TargetNewsId });

        modelBuilder.Entity<NewsLike>()
            .HasOne(nl => nl.SourceUser)
            .WithMany(u => u.LikedNews)
            .HasForeignKey(nl => nl.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NewsLike>()
            .HasOne(nl => nl.TargetNews)
            .WithMany(n => n.LikedByUsers)
            .HasForeignKey(nl => nl.TargetNewsId)
            .OnDelete(DeleteBehavior.Cascade);

    }

}
