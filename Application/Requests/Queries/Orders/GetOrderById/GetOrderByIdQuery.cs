using Application.Requests.Queries.Orders.Models;
using MediatR;

namespace Application.Requests.Queries.Orders.GetOrderById;

public class GetOrderByIdQuery : IRequest<OrderDto?>
{
    public int Id { get; set; }
}
