using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("ContactMessages")]
public class ContactMessageDao
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [Required]
    [MaxLength(200)]
    public required string Email { get; set; }
    
    [Required]
    [MaxLength(200)]
    public required string Subject { get; set; }
    
    [Required]
    [MaxLength(2000)]
    public required string Message { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsRead { get; set; } = false;
}
