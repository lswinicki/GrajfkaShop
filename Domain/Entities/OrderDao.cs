using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

[Table("Orders")]
public class OrderDao
{
    [Key]
    public int Id { get; set; }
    
    // Nullable — null for guest orders
    public int? UserId { get; set; }
    
    [Required]
    public DateTime OrderDate { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; }
    
    [Required]
    public PaymentStatus PaymentStatus { get; set; }
    
    // Guest & shared fields
    [MaxLength(200)]
    public string? GuestEmail { get; set; }
    
    [MaxLength(200)]
    public string? GuestName { get; set; }
    
    [MaxLength(50)]
    public string? GuestPhone { get; set; }
    
    [MaxLength(500)]
    public string? ShippingAddress { get; set; }
    
    [MaxLength(1000)]
    public string? CustomerNotes { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual UserDao? User { get; set; }
    public virtual ICollection<OrderItemDao> OrderItems { get; set; } = new List<OrderItemDao>();

    // Constructor for registered user orders
    [SetsRequiredMembers]
    public OrderDao(int? userId, DateTime orderDate, decimal totalAmount, OrderStatus status)
    {
        UserId = userId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
        PaymentStatus = PaymentStatus.Pending;
    }
    
    // Constructor for guest orders
    [SetsRequiredMembers]
    public OrderDao(string guestEmail, string guestName, DateTime orderDate, decimal totalAmount, OrderStatus status)
    {
        GuestEmail = guestEmail;
        GuestName = guestName;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
        PaymentStatus = PaymentStatus.Pending;
    }
    
    /// <summary>
    /// Returns the display name — either user email or guest name.
    /// </summary>
    public string GetCustomerDisplayName()
    {
        if (User != null)
            return User.Email ?? User.UserName ?? $"Klient #{UserId}";
        return GuestName ?? GuestEmail ?? "Gość";
    }
    
    /// <summary>
    /// Returns the email — user email or guest email.
    /// </summary>
    public string? GetCustomerEmail()
    {
        return User?.Email ?? GuestEmail;
    }
}

public enum OrderStatus
{
    [Display(Name = "Nowe")]
    New,
    
    [Display(Name = "W trakcie")]
    Pending,
    
    [Display(Name = "Zatwierdzone")]
    Approved,
    
    [Display(Name = "Anulowane")]
    Cancelled,
    
    [Display(Name = "Zrealizowane")]
    Done
}

public enum PaymentStatus
{
    [Display(Name = "Oczekuje")]
    Pending,
    
    [Display(Name = "Opłacone")]
    Paid,
    
    [Display(Name = "Nieudane")]
    Failed,
    
    [Display(Name = "Zwrócone")]
    Refunded
}
