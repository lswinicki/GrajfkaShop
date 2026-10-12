namespace Application.Requests.Queries.Orders.Models;

public class OrderDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string? ShippingAddress { get; set; }
    public string? CustomerNotes { get; set; }
    
    // Guest fields
    public string? GuestEmail { get; set; }
    public string? GuestName { get; set; }
    public string? GuestPhone { get; set; }
    
    /// <summary>Customer display name (user email or guest name)</summary>
    public string CustomerDisplayName { get; set; } = string.Empty;
    
    public bool IsGuestOrder => UserId == null;
    
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
