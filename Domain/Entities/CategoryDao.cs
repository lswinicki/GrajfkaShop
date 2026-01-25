using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

[Table("Categories")]
public class CategoryDao
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    
    public virtual ICollection<ProductDao>? Products { get; set; }

    [SetsRequiredMembers]
    public CategoryDao(string name)
    {
        Name = name;
    }
}