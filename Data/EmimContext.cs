using EMIM.Models;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Data;

public class EmimContext : DbContext
{
    public EmimContext(DbContextOptions<EmimContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>() 
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}
