using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Order.UpdateStatus;

public class UpdateOrderStatusHandler(IOrderRepository orderRepository)
    : IRequestHandler<UpdateOrderStatusCommand, int>
{
    public async Task<int> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Zamówienie o ID {request.Id} nie zostało znalezione.");

        order.Status = request.Status;
        order.PaymentStatus = request.PaymentStatus;

        return await orderRepository.UpdateAsync(order, cancellationToken);
    }
}
