using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Order.Add;

public class AddOrderHandler(IOrderRepository orderRepository) : IRequestHandler<AddOrderCommand, int>
{
    public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        return await orderRepository.AddAsync(new OrderDao(request.UserId, request.OrderDate, request.TotalAmount, request.Status), cancellationToken);
    }
}