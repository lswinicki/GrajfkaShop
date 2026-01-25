using MediatR;

namespace Application.Requests.Commands.Category.Update;

public class UpdateCategoryCommand : IRequest<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
}