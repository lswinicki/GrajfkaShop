using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces.Repositories;

public interface IBasicRepository<TEntity, TKey>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default
    );

    public Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetPagedAsync(
        int? pageNumber,
        int? pageSize,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool orderByDescending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    IExecutionStrategy CreateExecuteStrategy();
    Task <DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken);


    Task<IEnumerable<TKey>> GetMissingIdsAsync(List<TKey> ids, CancellationToken cancellationToken = default);
}