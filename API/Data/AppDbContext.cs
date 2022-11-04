using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class AppDbContext : DbContext
{
    public DbSet<AppUser> Users { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

}
