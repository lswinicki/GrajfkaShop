using Application.Requests.Queries.Products.Models;
using MediatR;

namespace Application.Requests.Queries.Products.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public int Id { get; set; }
}
