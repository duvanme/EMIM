using EMIM.Models;
using EMIM.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EMIM.Data;

public class EmimContext : IdentityDbContext<User>
{
    private readonly IConfiguration configuration;
    public EmimContext(DbContextOptions<EmimContext> options, IConfiguration configuration) : base(options)
    {
        this.configuration = configuration;
    }

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
            new Category { Id = 3, Name = "Tecnología" }
        );

        var adminUser = new User
        {
            Id = Guid.NewGuid().ToString(), 
            UserName = configuration["AdminUser:Email"],
            NormalizedUserName = configuration["AdminUser:Email"]?.ToUpper(),
            Email = configuration["AdminUser:Email"],
            NormalizedEmail = configuration["AdminUser:Email"]?.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(), 
            FirstName = "Admin", 
            LastName = "User",
            Status = Status.Active, 
            CreatedAt = DateTime.UtcNow, 
            ModifiedAt = DateTime.UtcNow 
        };

        var passwordHasher = new PasswordHasher<User>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, configuration["AdminUser:Password"]);

        modelBuilder.Entity<User>().HasData(adminUser);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = admin.Id,
                UserId = adminUser.Id
            }
        );


    }
}