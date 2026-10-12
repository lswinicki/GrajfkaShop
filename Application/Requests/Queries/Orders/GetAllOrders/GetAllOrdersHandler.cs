using Application.Common.Interfaces;
using Application.Requests.Queries.Orders.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.Orders.GetAllOrders;

public class GetAllOrdersHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Images)
            .OrderByDescending(o => o.OrderDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            Status = o.Status.ToString(),
            PaymentStatus = o.PaymentStatus.ToString(),
            ShippingAddress = o.ShippingAddress,
            CustomerNotes = o.CustomerNotes,
            GuestEmail = o.GuestEmail,
            GuestName = o.GuestName,
            GuestPhone = o.GuestPhone,
            CustomerDisplayName = o.GetCustomerDisplayName(),
            Items = o.OrderItems?.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "",
                ProductImageUrl = oi.Product?.Images?.FirstOrDefault()?.ImageUrl,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList() ?? new List<OrderItemDto>()
        });
    }
}
