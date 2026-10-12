using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Order.Update;

public class UpdateOrderHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderCommand, int>
{
    public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        order!.OrderDate = request.OrderDate;
        order.Status = request.Status;
        order.TotalAmount = request.TotalAmount;
        order.UserId = request.UserId;
        return await orderRepository.UpdateAsync(order, cancellationToken);
    }
}