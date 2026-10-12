using MediatR;

namespace Application.Requests.Commands.Products.Add;

public class AddProductCommand : IRequest<int>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public bool IsActive { get; set; } = true;
    public List<string>? ImageUrls { get; set; }
}
