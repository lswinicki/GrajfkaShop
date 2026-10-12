using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Order.PlaceOrder;

public class PlaceOrderHandler(IApplicationDbContext context)
    : IRequestHandler<PlaceOrderCommand, int>
{
    public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var totalAmount = request.Items.Sum(i => i.Price * i.Quantity);

        OrderDao order;

        if (request.UserId.HasValue)
        {
            order = new OrderDao(request.UserId, DateTime.UtcNow, totalAmount, OrderStatus.New);
        }
        else
        {
            order = new OrderDao(
                request.GuestEmail ?? "",
                request.GuestName ?? "",
                DateTime.UtcNow,
                totalAmount,
                OrderStatus.New);
            order.GuestPhone = request.GuestPhone;
        }

        order.ShippingAddress = request.ShippingAddress;
        order.CustomerNotes = request.CustomerNotes;

        foreach (var item in request.Items)
        {
            order.OrderItems.Add(new OrderItemDao
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            });
        }

        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
