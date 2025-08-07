using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<CategoryDao> Categories { get; set; }
    DbSet<ProductDao> Products { get; set; }
    DbSet<OrderDao> Orders { get; set; }
    DbSet<OrderItemDao> OrderItems { get; set; }
    DbSet<ProductImageDao> ProductImages { get; set; }
    DbSet<UserDao> Users { get; set; }
}