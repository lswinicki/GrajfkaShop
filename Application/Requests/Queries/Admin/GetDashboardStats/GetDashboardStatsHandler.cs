using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.Admin.GetDashboardStats;

public class GetDashboardStatsHandler(IApplicationDbContext context)
    : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        return new DashboardStatsDto
        {
            TotalProducts = await context.Products.CountAsync(cancellationToken),
            TotalOrders = await context.Orders.CountAsync(cancellationToken),
            TotalCategories = await context.Categories.CountAsync(cancellationToken),
            UnreadMessages = await context.ContactMessages.CountAsync(m => !m.IsRead, cancellationToken),
            TotalRevenue = await context.Orders
                .Where(o => o.PaymentStatus == PaymentStatus.Paid)
                .SumAsync(o => o.TotalAmount, cancellationToken),
            NewOrdersCount = await context.Orders
                .CountAsync(o => o.Status == OrderStatus.New, cancellationToken)
        };
    }
}
