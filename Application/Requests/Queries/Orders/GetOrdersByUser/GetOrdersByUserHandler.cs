using Application.Common.Interfaces.Repositories;
using Application.Requests.Queries.Orders.Models;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Queries.Orders.GetOrdersByUser;

public class GetOrdersByUserHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrdersByUserQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
    {
        var allOrders = new List<OrderDao>();

        // Fetch orders linked to user ID
        if (request.UserId.HasValue)
        {
            var userOrders = await orderRepository.GetByUserIdAsync(request.UserId.Value, cancellationToken);
            allOrders.AddRange(userOrders);
        }

        // Fetch guest orders by email (that aren't already linked to a user)
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var guestOrders = await orderRepository.GetByEmailAsync(request.Email, cancellationToken);
            var existingIds = allOrders.Select(o => o.Id).ToHashSet();
            allOrders.AddRange(guestOrders.Where(o => !existingIds.Contains(o.Id)));
        }

        return allOrders
            .OrderByDescending(o => o.OrderDate)
            .Select(MapToDto);
    }

    private static OrderDto MapToDto(OrderDao o) => new()
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
    };
}
