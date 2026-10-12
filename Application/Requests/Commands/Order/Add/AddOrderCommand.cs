using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Order.Add;

public class AddOrderCommand : IRequest<int>
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}