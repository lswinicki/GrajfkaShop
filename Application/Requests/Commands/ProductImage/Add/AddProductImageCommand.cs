using MediatR;

namespace Application.Requests.Commands.ProductImage.Add;

public class AddProductImageCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public required string ImageUrl { get; set; }
}
