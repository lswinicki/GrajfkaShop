using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Order.UpdateStatus;

public class UpdateOrderStatusCommand : IRequest<int>
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
