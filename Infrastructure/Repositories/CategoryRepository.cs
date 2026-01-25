using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistance;

namespace Infrastructure.Repositories;

public class CategoryRepository : BasicRepository<CategoryDao, int>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}