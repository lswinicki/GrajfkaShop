using Application.Common.Interfaces.Repositories;
using Application.Requests.Commands.Category.Models;
using MediatR;

namespace Application.Requests.Queries.Categories.GetAllCategories;

public class GetAllCategoriesHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(asNoTracking: true, cancellationToken);

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageUrl = c.ImageUrl
        });
    }
}
