using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Category.Update;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, int>
{
    public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (category == null)
            throw new KeyNotFoundException($"Kategoria o ID {request.Id} nie została znaleziona.");

        category.Name = request.Name;
        category.Description = request.Description;
        category.ImageUrl = request.ImageUrl;
        return await categoryRepository.UpdateAsync(category, cancellationToken);
    }
}