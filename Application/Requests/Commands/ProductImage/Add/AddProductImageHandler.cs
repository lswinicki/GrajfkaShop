using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.ProductImage.Add;

public class AddProductImageHandler(IApplicationDbContext context) : IRequestHandler<AddProductImageCommand, int>
{
    public async Task<int> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = new ProductImageDao
        {
            ProductId = request.ProductId,
            ImageUrl = request.ImageUrl
        };

        context.ProductImages.Add(image);
        return await context.SaveChangesAsync(cancellationToken);
    }
}
