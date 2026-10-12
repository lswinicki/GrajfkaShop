using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Products.Add;

public class AddProductHandler(IProductRepository productRepository) : IRequestHandler<AddProductCommand, int>
{
    public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductDao
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
            IsActive = request.IsActive,
            Images = request.ImageUrls?.Select(url => new ProductImageDao
            {
                ImageUrl = url
            }).ToList() ?? new List<ProductImageDao>()
        };

        return await productRepository.AddAsync(product, cancellationToken);
    }
}
