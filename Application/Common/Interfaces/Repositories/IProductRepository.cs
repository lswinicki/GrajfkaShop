using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IProductRepository : IBasicRepository<ProductDao, int>
{
    Task<ProductDao?> GetWithImagesAsync(int id, CancellationToken cancellationToken = default);
}