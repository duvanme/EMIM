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

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<SaleOrder> SaleOrders { get; set; }
    public DbSet<SaleOrderLine> SaleOrderLines { get; set; }
    public DbSet<SaleOrderStatus> SaleOrderStatuses { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<HelpQuestion> HelpQuestions { get; set;}



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Evitar eliminación en cascada en la relación Question - User
        modelBuilder.Entity<Question>()
            .HasOne(q => q.User)
            .WithMany()
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Evitar eliminación en cascada en la relación Question - Product
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Producto)
            .WithMany()
            .HasForeignKey(q => q.IdProducto)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasConversion<string>();


        string adminRoleId = "1";
        string customerRoleId = "2";
        string vendorRoleId = "3";

        var admin = new IdentityRole
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        };
        var customer = new IdentityRole
        {
            Id = customerRoleId,
            Name = "Customer",
            NormalizedName = "CUSTOMER"
        };
        var vendor = new IdentityRole
        {
            Id = vendorRoleId,
            Name = "Vendor",
            NormalizedName = "VENDOR"
        };

        modelBuilder.Entity<IdentityRole>().HasData(admin, customer, vendor);


        modelBuilder.Entity<Category>().HasData(

            new Category { Id = 1, Name = "Ropa" },
            new Category { Id = 2, Name = "Comida" },
            new Category { Id = 3, Name = "Tecnología" },
            new Category { Id = 4, Name = "Juguetes" },
            new Category { Id = 5, Name = "Deporte" },
            new Category { Id = 6, Name = "Maquillaje" },
            new Category { Id = 7, Name = "Hogar" },
            new Category { Id = 8, Name = "Libros" },
            new Category { Id = 9, Name = "Herramientas" }
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

        modelBuilder.Entity<SaleOrderLine>()
        .HasOne(d => d.SaleOrder)
        .WithMany(o => o.SaleOrderLine)
        .HasForeignKey(d => d.SaleOrderId) // Corregido: usa SaleOrderId
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SaleOrderLine>()
        .HasOne(d => d.Product)
        .WithMany(p => p.SaleOrderLine)
        .HasForeignKey(d => d.ProductId) // Corregido: usa ProductId
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SaleOrderStatus>()
        .HasOne(o => o.SaleOrder)
        .WithMany(s => s.SaleOrderStatus)
        .HasForeignKey(o => o.OrderId) // Corregido: usa OrderId
        .OnDelete(DeleteBehavior.Restrict);

        // Configuración para la tabla de favoritos
        modelBuilder.Entity<Favorite>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Favorite>()
    .HasOne(f => f.User)
    .WithMany()
    .HasForeignKey(f => f.UserId)
    .OnDelete(DeleteBehavior.Restrict); // Cambiado de Cascade a Restrict

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Product)
            .WithMany()
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Cambiado de Cascade a Restrict

    }
}
