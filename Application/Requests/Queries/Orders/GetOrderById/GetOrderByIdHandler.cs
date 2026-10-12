using Application.Common.Interfaces.Repositories;
using Application.Requests.Queries.Orders.Models;
using MediatR;

namespace Application.Requests.Queries.Orders.GetOrderById;

public class GetOrderByIdHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetWithItemsAsync(request.Id, cancellationToken);
        if (order == null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status.ToString(),
            PaymentStatus = order.PaymentStatus.ToString(),
            ShippingAddress = order.ShippingAddress,
            CustomerNotes = order.CustomerNotes,
            Items = order.OrderItems?.Select(oi => new OrderItemDto
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
}
