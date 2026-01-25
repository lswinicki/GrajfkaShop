using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Category.Add;

public class AddCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<AddCategoryCommand, int>
{
    public async Task<int> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await categoryRepository.AddAsync(new CategoryDao(request.Name), cancellationToken);
        
        return result;
    }
}