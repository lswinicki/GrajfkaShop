using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("OrderItems")]
public class OrderItemDao
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [ForeignKey(nameof(OrderId))]
    public virtual OrderDao Order { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public virtual ProductDao Product { get; set; }
}