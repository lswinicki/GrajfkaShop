using System.Data.Common;
using System.Linq.Expressions;
using Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public abstract class BasicRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BasicRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    // Get all with optional AsNoTracking
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
        return await query.ToListAsync(cancellationToken);
    }
    
    // Get with filter, include and tracking control
    public virtual async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        return await query.Where(filter).ToListAsync(cancellationToken);
    }
    
    // Get one with filter, include and tracking control
    public virtual async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        return await query.FirstOrDefaultAsync(filter);
    }

    // Get with pagination
    public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(
        int? pageNumber,
        int? pageSize,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool orderByDescending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (include != null)
            query = include(query);

        if (orderBy != null)
        {
            query = orderByDescending
                ? query.OrderByDescending(orderBy)
                : query.OrderBy(orderBy);
        }
        
        if(pageNumber.HasValue && pageSize.HasValue)
            return await query
            .Skip((pageNumber.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync(cancellationToken);
        else
            return await query
                .ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        return await query.CountAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync(id, cancellationToken);

    public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        if (_context.Entry(entity).Property("Created") != null)
        {
            _context.Entry(entity).Property("Created").IsModified = false;
        }
        
        var modifiedProp = _context.Entry(entity).Property("Modified");
        modifiedProp.CurrentValue = DateTime.UtcNow;
        
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IExecutionStrategy CreateExecuteStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        return transaction.GetDbTransaction();
    }

    public virtual async Task<IEnumerable<TKey>> GetMissingIdsAsync(List<TKey> ids, CancellationToken cancellationToken = default)
    {
        var existingIds = await _dbSet
            .Where(e => ids.Contains(EF.Property<TKey>(e, "Id")))
            .Select(e => EF.Property<TKey>(e, "Id"))
            .ToListAsync(cancellationToken);

        return ids.Except(existingIds).ToList();
    }

}