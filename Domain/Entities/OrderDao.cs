using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Orders")]
public class OrderDao
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    
    public DateTime OrderDate { get; set; }
    [Required]
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual UserDao User { get; set; }
    public virtual ICollection<OrderItemDao> OrderItems { get; set; }
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