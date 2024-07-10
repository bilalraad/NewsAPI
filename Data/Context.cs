using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Entities;

namespace NewsAPI;

public class Context(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, Guid,
                IdentityUserClaim<Guid>, AppUserRole,
                IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
                IdentityUserToken<Guid>>(options)
{

    public DbSet<News> News { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<NewsLike> NewsLikes { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NewsLike>()
            .HasKey(nl => new { nl.SourceUserId, nl.TargetNewsId });

        modelBuilder.Entity<AppUser>()
            .HasMany(nl => nl.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(nl => nl.UserId)
            .IsRequired();

        modelBuilder.Entity<AppRole>()
            .HasMany(nl => nl.UserRoles)
            .WithOne(n => n.Role)
            .HasForeignKey(nl => nl.RoleId)
            .IsRequired();

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


        modelBuilder.Entity<Category>()
            .HasMany(c => c.News)
            .WithOne(n => n.Category)
            .HasForeignKey(n => n.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);




    }

}
