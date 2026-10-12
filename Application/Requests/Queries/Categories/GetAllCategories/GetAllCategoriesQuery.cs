using Application.Requests.Commands.Category.Models;
using MediatR;

namespace Application.Requests.Queries.Categories.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
}
