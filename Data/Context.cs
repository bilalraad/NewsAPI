using Microsoft.EntityFrameworkCore;
using NewsAPI.Entities;

namespace NewsAPI;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Photo> Photos { get; set; }



}
