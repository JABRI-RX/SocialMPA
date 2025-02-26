using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Data;

public class ApplicationDbContext(DbContextOptions dbContextOptions) 
    : IdentityDbContext<AppUser>(dbContextOptions)
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Configuring Foriengs keys
        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

        builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);

        builder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);
        //Seeding Roles
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin", NormalizedName = "ADMIN",ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Name = "User", NormalizedName = "USER",ConcurrencyStamp = Guid.NewGuid().ToString()
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}