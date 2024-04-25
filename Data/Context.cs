using Microsoft.EntityFrameworkCore;
using NewsAPI.Entities;

namespace NewsAPI;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<News> News { get; set; }
}
