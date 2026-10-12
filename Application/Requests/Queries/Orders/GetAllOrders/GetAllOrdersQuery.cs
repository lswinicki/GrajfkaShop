using Application.Requests.Queries.Orders.Models;
using MediatR;

namespace Application.Requests.Queries.Orders.GetAllOrders;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}
