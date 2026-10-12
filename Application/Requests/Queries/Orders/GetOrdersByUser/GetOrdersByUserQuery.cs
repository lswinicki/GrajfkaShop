using Application.Requests.Queries.Orders.Models;
using MediatR;

namespace Application.Requests.Queries.Orders.GetOrdersByUser;

public class GetOrdersByUserQuery : IRequest<IEnumerable<OrderDto>>
{
    /// <summary>User ID for registered users</summary>
    public int? UserId { get; set; }
    
    /// <summary>Email for fetching linked guest orders too</summary>
    public string? Email { get; set; }
}
