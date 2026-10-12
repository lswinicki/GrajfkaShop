using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Category.Delete;

public class DeleteCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, int>
{
    public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
            throw new KeyNotFoundException($"Kategoria o ID {request.Id} nie została znaleziona.");

        return await categoryRepository.DeleteAsync(category, cancellationToken);
    }
}
