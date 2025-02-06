using Microsoft.EntityFrameworkCore;
using TheBox.Domain.Users.Entities;

namespace TheBox.Persistence.Users.DatabaseContext;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
    }
}