using EMIM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Data;

public class EmimContext : IdentityDbContext<User>
{
    public EmimContext(DbContextOptions<EmimContext> options) : base(options){}

    public DbSet<Category> Categories { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

      
        modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasConversion<string>();

      
        var admin = new IdentityRole("Admin") { NormalizedName = "ADMIN" };
        var customer = new IdentityRole("Customer") { NormalizedName = "CUSTOMER" };
        var vendor = new IdentityRole("Vendor") { NormalizedName = "VENDOR" };

        modelBuilder.Entity<IdentityRole>().HasData(admin, customer, vendor);

        
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Ropa" },
            new Category { Id = 2, Name = "Comida" },
            new Category { Id = 3, Name = "Tecnolog a" }
        );

    }
}