using Application.Common.Interfaces.Repositories;
using Application.Requests.Queries.Products.Models;
using MediatR;

namespace Application.Requests.Queries.Products.GetProductById;

public class GetProductByIdHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetWithImagesAsync(request.Id, cancellationToken);
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? "",
            IsActive = product.IsActive,
            ImageUrls = product.Images?.Select(i => i.ImageUrl).ToList() ?? new List<string>()
        };
    }
}
