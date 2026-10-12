using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Products.Delete;

public class DeleteProductHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, int>
{
    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Produkt o ID {request.Id} nie został znaleziony.");

        return await productRepository.DeleteAsync(product, cancellationToken);
    }
}
