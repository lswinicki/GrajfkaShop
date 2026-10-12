using MediatR;

namespace Application.Requests.Commands.OrderItem.Update;

public class UpdateOrderItemCommand : IRequest<int>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}