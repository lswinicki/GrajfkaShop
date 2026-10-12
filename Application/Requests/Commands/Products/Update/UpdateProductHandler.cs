using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Products.Update;

public class UpdateProductHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, int>
{
    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Produkt o ID {request.Id} nie został znaleziony.");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        product.IsActive = request.IsActive;

        return await productRepository.UpdateAsync(product, cancellationToken);
    }
}
