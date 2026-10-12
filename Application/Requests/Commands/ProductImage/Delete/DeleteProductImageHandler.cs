using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Commands.ProductImage.Delete;

public class DeleteProductImageHandler(IApplicationDbContext context) : IRequestHandler<DeleteProductImageCommand, int>
{
    public async Task<int> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = await context.ProductImages.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
        if (image == null)
            throw new KeyNotFoundException($"Zdjęcie o ID {request.Id} nie zostało znalezione.");

        context.ProductImages.Remove(image);
        return await context.SaveChangesAsync(cancellationToken);
    }
}
