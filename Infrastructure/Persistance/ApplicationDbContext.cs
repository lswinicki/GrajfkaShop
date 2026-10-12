using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class ApplicationDbContext : IdentityDbContext<UserDao, IdentityRole<int>, int>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<CategoryDao> Categories { get; set; }
    public DbSet<ProductDao> Products { get; set; }
    public DbSet<OrderDao> Orders { get; set; }
    public DbSet<OrderItemDao> OrderItems { get; set; }
    public DbSet<ProductImageDao> ProductImages { get; set; }
    public DbSet<UserDao> Users { get; set; }
    public DbSet<ContactMessageDao> ContactMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CategoryDao>().HasData(
            new CategoryDao("Zaproszenia Ślubne") { Id = 1 },
            new CategoryDao("Zaproszenia Komunijne") { Id = 2 },
            new CategoryDao("Zaproszenia Urodzinowe") { Id = 3 },
            new CategoryDao("Biżuteria") { Id = 4 }
        );
    }
}