using EMIM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Data;

public class EmimContext : IdentityDbContext<User>
{
    public EmimContext(DbContextOptions<EmimContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
            modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

            var admin = new IdentityRole("Admin");
            admin.NormalizedName = "Admin";

            var customer = new IdentityRole("Customer");
            customer.NormalizedName = "Customer";

            var vendor = new IdentityRole("Vendor");
            vendor.NormalizedName = "Vendor";

            modelBuilder.Entity<IdentityRole>()
            .HasData(admin, customer, vendor);
    }
}
