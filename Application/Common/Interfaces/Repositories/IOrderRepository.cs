using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IOrderRepository : IBasicRepository<OrderDao, int>
{
    Task<IEnumerable<OrderDao>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderDao>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<OrderDao?> GetWithItemsAsync(int id, CancellationToken cancellationToken = default);
    Task LinkGuestOrdersToUserAsync(string email, int userId, CancellationToken cancellationToken = default);
}