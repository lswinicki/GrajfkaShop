using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Order.Update;

public class UpdateOrderCommand : IRequest<int>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}