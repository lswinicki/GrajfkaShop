using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Category.Update;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, int>
{
    public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        category!.Name = request.Name;
        return await categoryRepository.UpdateAsync(category, cancellationToken);
    }
}