using MediatR;

namespace Application.Requests.Commands.ProductImage.Delete;

public class DeleteProductImageCommand : IRequest<int>
{
    public int Id { get; set; }
}
