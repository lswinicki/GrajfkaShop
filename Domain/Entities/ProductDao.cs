using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Products")]
public class ProductDao
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    public required bool IsActive { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    public virtual CategoryDao Category { get; set; }
    
    public virtual ICollection<ProductImageDao> Images { get; set; }

    public virtual ICollection<OrderItemDao> OrderItems { get; set; }
}