using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository : IBasicRepository<UserDao, int>
{
    Task<UserDao?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<UserDao?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}