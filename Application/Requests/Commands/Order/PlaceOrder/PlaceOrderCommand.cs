using MediatR;

namespace Application.Requests.Commands.Order.PlaceOrder;

public class PlaceOrderCommand : IRequest<int>
{
    // For registered users
    public int? UserId { get; set; }
    
    // For guests
    public string? GuestEmail { get; set; }
    public string? GuestName { get; set; }
    public string? GuestPhone { get; set; }
    
    // Shared
    public string ShippingAddress { get; set; } = string.Empty;
    public string? CustomerNotes { get; set; }
    
    public List<PlaceOrderItem> Items { get; set; } = new();
}

public class PlaceOrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
