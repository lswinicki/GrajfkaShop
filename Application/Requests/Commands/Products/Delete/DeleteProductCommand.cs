using MediatR;

namespace Application.Requests.Commands.Products.Delete;

public class DeleteProductCommand : IRequest<int>
{
    public int Id { get; set; }
}
