using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("ProductImages")]
public class ProductImageDao
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public string ImageUrl { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public virtual ProductDao Product { get; set; }
}