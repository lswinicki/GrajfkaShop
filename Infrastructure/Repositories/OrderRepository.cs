using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : BasicRepository<OrderDao, int>, IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDao>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Images)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderDao>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.ToLower();
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Images)
            .Where(o => o.GuestEmail != null && o.GuestEmail.ToLower() == normalizedEmail)
            .OrderByDescending(o => o.OrderDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderDao?> GetWithItemsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Images)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Links all guest orders with matching email to the given user ID.
    /// Called after user login/registration to merge guest order history.
    /// </summary>
    public async Task LinkGuestOrdersToUserAsync(string email, int userId, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.ToLower();
        var guestOrders = await _context.Orders
            .Where(o => o.UserId == null
                        && o.GuestEmail != null
                        && o.GuestEmail.ToLower() == normalizedEmail)
            .ToListAsync(cancellationToken);

        foreach (var order in guestOrders)
        {
            order.UserId = userId;
        }

        if (guestOrders.Any())
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}